using EmployeeManagementSystem.Application.DTOs.Employee;
using FluentValidation;

namespace EmployeeManagementSystem.Application.Validators.Employee;

public class CreateEmployeeValidator
    : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid Email Address.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty();

        RuleFor(x => x.Salary)
            .GreaterThan(0);

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0);

        RuleFor(x => x.HireDate)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Hire Date cannot be in the future.");
    }
}