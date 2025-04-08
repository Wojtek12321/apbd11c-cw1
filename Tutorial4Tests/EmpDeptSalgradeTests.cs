﻿using Tutorial3.Models;

public class EmpDeptSalgradeTests
{
    // 1. Simple WHERE filter
    // SQL: SELECT * FROM Emp WHERE Job = 'SALESMAN';
    [Fact]
    public void ShouldReturnAllSalesmen()
    {
        var emps = Database.GetEmps();

        List<Emp> result = emps
        .Where(e => e.Job == "SALESMAN")
        .ToList(); 

        foreach (var emp in result)
        {
            Console.WriteLine($"{emp.EmpNo}");
        }

        Assert.Equal(2, result.Count);
        Assert.All(result, e => Assert.Equal("SALESMAN", e.Job));
    }

    // 2. WHERE + OrderBy
    // SQL: SELECT * FROM Emp WHERE DeptNo = 30 ORDER BY Sal DESC;
    [Fact]
    public void ShouldReturnDept30EmpsOrderedBySalaryDesc()
    {
        var emps = Database.GetEmps();

        List<Emp> result = emps
        .Where(e => e.DeptNo == 30)
        .OrderByDescending(e => e.Sal)
        .ToList();

        foreach (var emp in result)
        {
            Console.WriteLine($"EmpNo: {emp.EmpNo}, EName: {emp.EName}, Sal: {emp.Sal}");
        }

        Assert.Equal(2, result.Count);
        Assert.True(result[0].Sal >= result[1].Sal);
    }
   
    // 3. Subquery using LINQ (IN clause)
    // SQL: SELECT * FROM Emp WHERE DeptNo IN (SELECT DeptNo FROM Dept WHERE Loc = 'CHICAGO');
    [Fact]
    public void ShouldReturnEmployeesFromChicago()
    {
        var emps = Database.GetEmps();
        var depts = Database.GetDepts();

        var chicagoDeptNos = depts
        .Where(d => d.Loc == "CHICAGO")
        .Select(d => d.DeptNo);

        List<Emp> result = emps
        .Where(e => chicagoDeptNos.Contains(e.DeptNo))
        .ToList();

        foreach (var emp in result)
        {
            Console.WriteLine($"{emp.EmpNo} | {emp.EName} | {emp.Job} | {emp.Sal} | Dept: {emp.DeptNo}");
        }

        Assert.All(result, e => Assert.Equal(30, e.DeptNo));
    }

    // 4. SELECT projection
    // SQL: SELECT EName, Sal FROM Emp;
    [Fact]
    public void ShouldSelectNamesAndSalaries()
    {
        var emps = Database.GetEmps();

        var result = emps
        .Select(e => new { e.EName, e.Sal })
        .ToList();

        foreach (var emp in result)
        {
            Console.WriteLine($"{emp.EName} | {emp.Sal}");
        }

        Assert.All(result, r =>
         {
             Assert.False(string.IsNullOrWhiteSpace(r.EName));
             Assert.True(r.Sal > 0);
         });
    }

    // 5. JOIN Emp to Dept
    // SQL: SELECT E.EName, D.DName FROM Emp E JOIN Dept D ON E.DeptNo = D.DeptNo;
    [Fact]
    public void ShouldJoinEmployeesWithDepartments()
    {
        var emps = Database.GetEmps();
        var depts = Database.GetDepts();

        var result = emps
        .Join(depts, e => e.DeptNo, d => d.DeptNo, (e, d) => new { e.EName, d.DName })
        .ToList();

        foreach (var r in result)
        {
            Console.WriteLine($"{r.EName} | {r.DName}");
        }

        Assert.Contains(result, r => r.DName == "SALES" && r.EName == "ALLEN");

    }

    // 6. Group by DeptNo
    // SQL: SELECT DeptNo, COUNT(*) FROM Emp GROUP BY DeptNo;
    [Fact]
    public void ShouldCountEmployeesPerDepartment()
    {
        var emps = Database.GetEmps();

        var result = emps
        .GroupBy(e => e.DeptNo)
        .Select(g => new { DeptNo = g.Key, Count = g.Count() })  
        .ToList();

        foreach (var r in result)
        {
            Console.WriteLine($"DeptNo: {r.DeptNo} | Employees: {r.Count}");
        }

        Assert.Contains(result, g => g.DeptNo == 30 && g.Count == 2);
        Assert.All(result, g => Assert.True(g.Count >= 0));
    }

    // 7. SelectMany (simulate flattening)
    // SQL: SELECT EName, Comm FROM Emp WHERE Comm IS NOT NULL;
    [Fact]
    public void ShouldReturnEmployeesWithCommission()
    {
        var emps = Database.GetEmps();

        var result = emps
        .Where(e => e.Comm != null) 
        .Select(e => new { e.EName, e.Comm }) 
        .ToList();

        foreach (var r in result)
        {
            Console.WriteLine($"Employee: {r.EName} | Commission: {r.Comm}");
        }

         Assert.All(result, r => Assert.NotNull(r.Comm));
    }

    // 8. Join with Salgrade
    // SQL: SELECT E.EName, S.Grade FROM Emp E JOIN Salgrade S ON E.Sal BETWEEN S.Losal AND S.Hisal;
    [Fact]
    public void ShouldMatchEmployeeToSalaryGrade()
    {
        var emps = Database.GetEmps();
        var grades = Database.GetSalgrades();

        //var result = emps
        //
        //Assert.Contains(result, r => r.DeptNo == 30 && r.AvgSal > 1000);
    }

    // 9. Aggregation (AVG)
    // SQL: SELECT DeptNo, AVG(Sal) FROM Emp GROUP BY DeptNo;
    [Fact]
    public void ShouldCalculateAverageSalaryPerDept()
    {
        var emps = Database.GetEmps();

        var result = emps
        .GroupBy(e => e.DeptNo)
        .Select(g => new { DeptNo = g.Key, AvgSal = g.Average(e => e.Sal) })
        .ToList();

        foreach (var r in result)
        {
            Console.WriteLine($"DeptNo: {r.DeptNo} | Average Salary: {r.AvgSal}");
        }


        Assert.Contains(result, r => r.DeptNo == 30 && r.AvgSal > 1000);
        Assert.Contains(result, r => r.DeptNo == 10 && r.AvgSal == 5000);
    }

    // 10. Complex filter with subquery and join
    // SQL: SELECT E.EName FROM Emp E WHERE E.Sal > (SELECT AVG(Sal) FROM Emp WHERE DeptNo = E.DeptNo);
    [Fact]
    public void ShouldReturnEmployeesEarningMoreThanDeptAverage()
    {
        var emps = Database.GetEmps();

        var result = emps
        .Where(e => e.Sal > emps
        .Where(emp => emp.DeptNo == e.DeptNo) 
        .Average(emp => emp.Sal))  
        .Select(e => e.EName)  
        .ToList();

        foreach (var eName in result)
        {
            Console.WriteLine($"Employee: {eName}");
        }

        Assert.Contains("ALLEN", result);
    }
}
