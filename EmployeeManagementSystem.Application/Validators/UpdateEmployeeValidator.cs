using EmployeeManagementSystem.Application.DTOs.Employee;
using FluentValidation;

namespace EmployeeManagementSystem.Application.Validators.Employee;

public class UpdateEmployeeValidator
    : AbstractValidator<UpdateEmployeeDto>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty();

        RuleFor(x => x.Salary)
            .GreaterThan(0);

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0);
    }
}