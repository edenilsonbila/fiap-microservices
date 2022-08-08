using GeekBurger.LabelLoader.Helper;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace GeekBurger.LabelLoader
{
    public class VisionService
    {
        private static ConfigHelper _configHelper = new ConfigHelper();
        public static string[] blacklist = new string[]
            { "ingredients", "processed in a facility that handles", "products" , "allergens" , "contains" };
        public const string AndWithSpace = " and ";
        public const string CommaWithSpace = " , ";

        public async Task<List<string>>  ReadIngredientsFromImage(string imageFilePath)
        {
            var visionServiceClient = Authenticate("https://labelloadervision.cognitiveservices.azure.com", _configHelper.GetConfigString("VisionAPIKey"));

            var textHeaders = await visionServiceClient.ReadInStreamAsync(File.OpenRead(imageFilePath));

            string operationLocation = textHeaders.OperationLocation;

            const int numberOfCharsInOperationId = 36;
            string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

            // Extract the text
            ReadOperationResult results;
            do
            {
                results = await visionServiceClient.GetReadResultAsync(Guid.Parse(operationId));
            }
            while ((results.Status == OperationStatusCodes.Running ||
                results.Status == OperationStatusCodes.NotStarted));

            var textUrlFileResults = results.AnalyzeResult.ReadResults;

            var listString = new List<string>();
            foreach (ReadResult page in textUrlFileResults)
            {
                foreach (Line line in page.Lines)
                {
                    listString.Add(line.Text.ToLower());
                }
            }

            return listString;
        }

        public ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }
    }
}
