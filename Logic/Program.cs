using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class TestModel
    {
        public List<SubTestModel> SubTest { get; set; }
    }

    public class SubTestModel
    {
        public DocumentType Document { get; set; }
    }

    public enum DocumentType
    {
        Insurance = 1,
        MOT,
        VehicleRegistration
    }

    public enum ApiValidationStatus
    {
        Passed = 1,
        Failed
    }

    class Program
    {
        public enum Check
        {
            Truck = 1,
            Marketer
        }

        static DocumentType[] documentTypes = new DocumentType[] { DocumentType.Insurance, DocumentType.MOT};
        static DocumentType[] documentTypes2 = new DocumentType[] { DocumentType.MOT, DocumentType.VehicleRegistration, DocumentType.Insurance};
        static ApiValidationStatus[] documentTypes3 = new ApiValidationStatus[] { ApiValidationStatus.Passed, ApiValidationStatus.Passed};

        static void Main(string[] args)
        {
            if (DocumentCheck())
            {
                Console.WriteLine("Truck Request Passed");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Truck Request Failed");
                Console.ReadLine();
            }
        }

        

        private static bool DocumentTypeCheck()
        {
            for (int i = 0; i < documentTypes2.Count(); i++)
            {
                if (!documentTypes.Contains(documentTypes2[i]))
                    return false;
            }
            return true;
        }

        private static bool DocumentCheck()
        {
            var docArray = documentTypes3;
            for (int i = 0; i < documentTypes3.Count(); i++)
            {
                if (docArray[i] == ApiValidationStatus.Failed)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
