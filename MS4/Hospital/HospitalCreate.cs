using MS4;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital
{
    public class HospitalCreate: Element
    {
        Random random = new Random();
        public List<PatientType> PatientsTypes { get; set; }
        public PatientType patientType { get; set; }
        public HospitalCreate(double delay, string name, string distribution, double devDelay=0) : base(delay, name, distribution, devDelay)
        {
            PatientsTypes = new List<PatientType>();
            patientType = new PatientType(0, 0, 0);
        }
        public override void OutAct()
        {
            base.OutAct();
            int index = 0;
            if (PatientsTypes.Count > 0)
            {
                index = patientType.ChooseProbability(PatientsTypes) - 1;
                foreach (var t in PatientsTypes)
                {
                    if (index + 1 == t.Index)
                    {
                        t.Quantity++;
                    }
                }
            }

            TNext = TCurrent + GetDelay();

            NextElement.AverageDelay = PatientsTypes[index].AvgTimeOfRegistration;
            NextElement.InAct(PatientsTypes[index].Index);
        }
    }
}
