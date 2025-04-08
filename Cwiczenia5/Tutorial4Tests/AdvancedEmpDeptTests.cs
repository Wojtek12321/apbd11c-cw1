namespace Tutorial3Tests;

public class AdvancedEmpDeptTests
{
    // 11. MAX salary
    // SQL: SELECT MAX(Sal) FROM Emp;
    [Fact]
    public void ShouldReturnMaxSalary()
    {
        var emps = Database.GetEmps();

        decimal? maxSalary = emps.Max(e => e.Sal);

        Console.WriteLine($"Max Salary: {maxSalary}");

        Assert.Equal(5000, maxSalary);
    }

    // 12. MIN salary in department 30
    // SQL: SELECT MIN(Sal) FROM Emp WHERE DeptNo = 30;
    [Fact]
    public void ShouldReturnMinSalaryInDept30()
    {
        var emps = Database.GetEmps();

        decimal? minSalary = emps
        .Where(e => e.DeptNo == 30)
        .Min(e => e.Sal);

        Console.WriteLine($"Min Salary in Dept 30: {minSalary}");

        Assert.Equal(1250, minSalary);
    }

    // 13. Take first 2 employees ordered by hire date
    // SQL: SELECT * FROM Emp ORDER BY HireDate ASC FETCH FIRST 2 ROWS ONLY;
    [Fact]
    public void ShouldReturnFirstTwoHiredEmployees()
    {
        var emps = Database.GetEmps();

        var firstTwo = emps
        .OrderBy(e => e.HireDate) 
        .Take(2) 
        .ToList();

        foreach (var emp in firstTwo)
        {
            Console.WriteLine($"{emp.EmpNo} | {emp.EName} | {emp.HireDate}");
        }

        
        Assert.Equal(2, firstTwo.Count);
        Assert.True(firstTwo[0].HireDate <= firstTwo[1].HireDate);
    }

    // 14. DISTINCT job titles
    // SQL: SELECT DISTINCT Job FROM Emp;
    [Fact]
    public void ShouldReturnDistinctJobTitles()
    {
        var emps = Database.GetEmps();

        var jobs = emps
        .Select(e => e.Job)  
        .Distinct() 
        .ToList();

        foreach (var job in jobs)
        {
            Console.WriteLine($"Job: {job}");
        }

        Assert.Contains("PRESIDENT", jobs);
        Assert.Contains("SALESMAN", jobs);
    }

    // 15. Employees with managers (NOT NULL Mgr)
    // SQL: SELECT * FROM Emp WHERE Mgr IS NOT NULL;
    [Fact]
    public void ShouldReturnEmployeesWithManagers()
    {
        var emps = Database.GetEmps();

        var withMgr = emps
        .Where(e => e.Mgr != null)
        .ToList();

        foreach (var emp in withMgr)
        {
            Console.WriteLine($"{emp.EmpNo} | {emp.EName} | Manager: {emp.Mgr}");
        }

        Assert.Equal(4, withMgr.Count);
        Assert.All(withMgr, e => Assert.NotNull(e.Mgr));
    }

    // 16. All employees earn more than 500
    // SQL: SELECT * FROM Emp WHERE Sal > 500; (simulate all check)
    [Fact]
    public void AllEmployeesShouldEarnMoreThan500()
    {
        var emps = Database.GetEmps();

        var result = emps.All(e => e.Sal > 500);

        foreach (var emp in emps)
        {
            Console.WriteLine($"{emp.EmpNo} | {emp.EName} | Salary: {emp.Sal}");
        }

        
        Assert.True(result);
    }

    // 17. Any employee with commission over 400
    // SQL: SELECT * FROM Emp WHERE Comm > 400;
    [Fact]
    public void ShouldFindAnyWithCommissionOver400()
    {
        var emps = Database.GetEmps();

        var result = emps.Any(e => e.Comm > 400);

        foreach (var emp in emps)
        {
            Console.WriteLine($"{emp.EmpNo} | {emp.EName} | Comm: {emp.Comm}");
        }

        Assert.True(result);
    }

    // 18. Self-join to get employee-manager pairs
    // SQL: SELECT E1.EName AS Emp, E2.EName AS Manager FROM Emp E1 JOIN Emp E2 ON E1.Mgr = E2.EmpNo;
    [Fact]
    public void ShouldReturnEmployeeManagerPairs()
    {
        var emps = Database.GetEmps();

        var result = from e1 in emps
                     join e2 in emps on e1.Mgr equals e2.EmpNo
                     select new{ Employee = e1.EName, Manager = e2.EName};

        foreach (var pair in result)
        {
            Console.WriteLine($"Employee: {pair.Employee}, Manager: {pair.Manager}");
        }

        Assert.Contains(result, r => r.Employee == "SMITH" && r.Manager == "FORD");
    }

    // 19. Let clause usage (sal + comm)
    // SQL: SELECT EName, (Sal + COALESCE(Comm, 0)) AS TotalIncome FROM Emp;
    [Fact]
    public void ShouldReturnTotalIncomeIncludingCommission()
    {
        var emps = Database.GetEmps();

        var result = emps
        .Select(e => new{ e.EName, Total = e.Sal + (e.Comm ?? 0)})
        .ToList();

        foreach (var r in result)
        {
            Console.WriteLine($"Employee: {r.EName}, Total Income: {r.Total}");
        }
        Assert.Contains(result, r => r.EName == "ALLEN" && r.Total == 1900);
    }

    // 20. Join all three: Emp → Dept → Salgrade
    // SQL: SELECT E.EName, D.DName, S.Grade FROM Emp E JOIN Dept D ON E.DeptNo = D.DeptNo JOIN Salgrade S ON E.Sal BETWEEN S.Losal AND S.Hisal;
    [Fact]
    public void ShouldJoinEmpDeptSalgrade()
    {
        var emps = Database.GetEmps();
        var depts = Database.GetDepts();
        var grades = Database.GetSalgrades();

        // var result = null; 
        //
        // Assert.Contains(result, r => r.EName == "ALLEN" && r.DName == "SALES" && r.Grade == 3);
    }
}
