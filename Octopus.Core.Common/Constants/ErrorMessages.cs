namespace Octopus.Core.Common.Constants
{
    public static class ErrorMessages
    {
        public const string EmptyRequest = "No input data provided";

        public const string InputFileNotExist = "Input file does not exist";

        public const string InputFileNotAvailable = "Input file does not available";

        public const string NoDescriptionProvided = "No description provided for expected model";

        public const string QueueException = "An exception was thrown during the work with queue. The inner exception is ";

        public const string CsvParserException = "An exception was thrown during the parsing of the input .csv file. The inner exception is ";

        public const string JsonParserException = "An exception was thrown during the parsing of the input .json file. The inner exception is ";

        public const string XmlParserException = "An exception was thrown during the parsing of the input .xml file. The inner exception is ";

        public const string DynamicServiceException = "An exception was thrown during the work with dynamic service exception. The inner exception is ";

        public const string UnhandledException = "An unhandled exception was thrown. The inner exception is ";
    }
}
