using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace WebApi.Utilities.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(ApartmentDto).IsAssignableFrom(type) ||
                typeof(IEnumerable<ApartmentDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        private static void FormatCsv(StringBuilder buffer, ApartmentDto apartmentDto)
        {
            buffer.AppendLine($"{apartmentDto.Id}, {apartmentDto.No}, {apartmentDto.Type},{apartmentDto.Status},{apartmentDto.Floor}");
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<ApartmentDto>)
            {
                foreach (var book in (IEnumerable<ApartmentDto>)context.Object)
                {
                    FormatCsv(buffer, book);
                }
            }
            else
            {
                FormatCsv(buffer, (ApartmentDto)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }
    }
}
