using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.AS400.Environments
{
    public class Retriever
    {
        public Retriever(DateTime? udate)
        {
            Job = JobAttribute.Of(udate);
        }

        public static Retriever Instance = new Retriever(null);

        public JobAttribute Job;

        public NetworkAttribute Network = new NetworkAttribute();

        public SystemValue System = SystemValue.Of();

        public DataAreaSingleValues DataAreaSingleValues = new DataAreaSingleValues();

    }
}
