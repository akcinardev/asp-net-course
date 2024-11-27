using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationExample.Models;

namespace ModelValidationExample.CustomModelBinders
{
    public class PersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Person person = new Person();

            // FirstName and LastName
            if(bindingContext.ValueProvider.GetValue("FirstName").Length > 0)
            {
                person.Name = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;

                if (bindingContext.ValueProvider.GetValue("LastName").Length > 0)
                {
                    person.Name += " " + bindingContext.ValueProvider.GetValue("LastName").FirstValue;
                }
            }

            //Email
            if (bindingContext.ValueProvider.GetValue("Email").Length > 0)
            {
                person.Email = bindingContext.ValueProvider.GetValue("Email").FirstValue;
            }

            //Password
            if (bindingContext.ValueProvider.GetValue("Password").Length > 0)
            {
                person.Password
                    = bindingContext.ValueProvider.GetValue("Password").FirstValue;
            }

            //ConfirmPassword
            if (bindingContext.ValueProvider.GetValue("ConfirmPassword").Length > 0)
            {
                person.ConfirmPassword = bindingContext.ValueProvider.GetValue("ConfirmPassword").FirstValue;
            }

            bindingContext.Result = ModelBindingResult.Success(person);
            return Task.CompletedTask;
        }
    }
}
