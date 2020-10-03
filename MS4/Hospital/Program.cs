using MS4;
using System;
using System.Collections.Generic;

namespace Hospital
{
    class Program
    {
        static void Main(string[] args)
        {
            //HospitalCreate c = new HospitalCreate(15, "CREATOR", "Exponential");

            //Doctor p0 = new Doctor(5, 8, "DOCTOR", "Exponential", 2);

            ////'''час следования в палату
            ////равномерно от 3 до 8 и следующая 3 это сопровождающие'''
            //HospitalProcess p1 = new HospitalProcess(3, 8, "CHAMBERS", "Exponential", 6);

            ////'''час обслуживания в регестратуре лаборатории
            //// мат.ожидание - 4.5
            ////коэфф. - 3'''
            //HospitalProcess p2 = new HospitalProcess(3, 4.5, "REGISTRATION", "Exponential", 1);

            ////'''час проведения анализа
            ////  мат.ожидание - 4
            //// коэфф. - 2'''
            //Laboratory p3 = new Laboratory(2, 4, "LABORATORY", "Exponential", 2);

            //c.NextElement = p0;
            //p0.NextProcesses = new List<HospitalProcess> { p1, p2 };
            //p2.NextProcesses = new List<HospitalProcess> { p3 };
            //p3.NextProcesses = new List<HospitalProcess> { p0 };

            //List<Element> elementsList = new List<Element> { c, p0, p1, p2, p3 };
            //HospitalModel model = new HospitalModel(elementsList);
            //model.Simulate(1000.0);

            HospitalCreate c = new HospitalCreate(10, "CREATOR", "Exponential");
            HospitalProcess p0 = new HospitalProcess(2, 5, "GO TO DOCTOR", "Uniform", 3);
            HospitalProcess p1 = new HospitalProcess(3, 8, "CHAMBERS", "Uniform", 3);
            HospitalProcess d = new HospitalProcess(3, 8, "EXIT", "Exponential", 3);
            HospitalProcess p2 = new HospitalProcess(3, 4.5, "REGISTRATION", "Erlang", 4);
            Laboratory p3 = new Laboratory(2, 4, "LABORATORY", "Erlang", 2);
            HospitalProcess p4 = new HospitalProcess(2, 5, "GO TO REGISTRATION", "Exponential", 3);
            Doctor p5 = new Doctor(2, 5, "DOCTOR", "Uniform", 2);

            c.NextElement = p0;
            c.PatientsTypes = new List<PatientType>{
                new PatientType(1, 0.5, 1/15),
                new PatientType(2, 0.1, 1/40),
                new PatientType(3, 0.4, 1/30)
            };
            p0.NextProcesses = new List<HospitalProcess> { p5 };
            p5.NextProcesses = new List<HospitalProcess> { p1, p4 };
            p2.NextProcesses = new List<HospitalProcess> { p3 };
            p3.NextProcesses = new List<HospitalProcess> { p0, d };
            p4.NextProcesses = new List<HospitalProcess> { p2 };

            List<Element> elementsList = new List<Element> { c, p0, p1, p2, p3, p4, p5, d };
            HospitalModel model = new HospitalModel(elementsList);
            model.Simulate(1000.0);
        }
    }
}
