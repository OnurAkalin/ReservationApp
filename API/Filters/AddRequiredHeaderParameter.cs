namespace API.Filters;

public class AddRequiredHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "SiteId",
            In = ParameterLocation.Header,
            Description = "Current Site Id",
            Required = false
        });
    }
}