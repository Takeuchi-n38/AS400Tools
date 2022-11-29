namespace Delta.AS400.Tests.DBAssertions
{
    public class ConversionProgram
    {

        public string programPackage;
        public string programSchema;
        public string programId;

        public ConversionProgram(string aProgramPackage, string aProgramSchema, string aProgramId)
        {
            programPackage = aProgramPackage;
            programSchema = aProgramSchema;
            programId = aProgramId;
        }

        public static ConversionProgram CreateBy(string aProgramPackage, string aProgramSchema, string aProgramId)
        {
            return new ConversionProgram(aProgramPackage, aProgramSchema, aProgramId);
        }

        public ConversionProgram CreateBy(string packageName)
        {
            return new ConversionProgram(packageName, ProgramSchemaName(packageName), Method_programId(packageName));
        }

        private string Method_programId(string packageName)
        {
            var partOfPackage = packageName.Split("\\.");
            //jp.co.deltakogyo.honsha01.seatlib.domain.service.pjike14
            return partOfPackage[partOfPackage.Length - 1];
        }

        private string ProgramSchemaName(string packageName)
        {
            var partOfPackage = packageName.Split("\\.");
            //jp.co.deltakogyo.honsha01.seatlib.domain.service.pjike14
            return partOfPackage[4];
        }

    }

}
