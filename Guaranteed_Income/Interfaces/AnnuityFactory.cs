using Guaranteed_Income.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Models
{
    public class AnnuityFactory : Annuities
    {
        public AnnuityFactory(AnnuityTax tax, AnnuityTime time, AnnuityRate rate, Person person, Brokerage stock) : base(person)
        {
            //Set up annuity details based on type
            switch (tax)
            {
                case AnnuityTax.Nonqualified:
                    NonQualified();
                    switch (time)
                    {
                        case AnnuityTime.Deferred:
                            Deferred();
                            switch (rate)
                            {
                                //Non-qualified deferred fixed annuity
                                case AnnuityRate.Fixed:
                                    Fixed();
                                    break;
                                //Non-qualified deferred variable annuity
                                case AnnuityRate.Variable:
                                    Variable();
                                    break;
                            }
                            break;
                        case AnnuityTime.Immediate:
                            Immediate();
                            switch (rate)
                            {
                                //Non-qualified immediate fixed annuity
                                case AnnuityRate.Fixed:
                                    Fixed();
                                    break;
                                //Non-qualified immediate variable annuity
                                case AnnuityRate.Variable:
                                    Variable();
                                    break;
                            }
                            break;
                    }
                    break;
                case AnnuityTax.Qualified:
                    Qualified();
                    switch (time)
                    {
                        case AnnuityTime.Deferred:
                            Deferred();
                            switch (rate)
                            {
                                //qualified deferred fixed annuity
                                case AnnuityRate.Fixed:
                                    Fixed();
                                    break;
                                //qualified deferred variable annuity
                                case AnnuityRate.Variable:
                                    Variable();
                                    break;
                            }
                            break;
                        case AnnuityTime.Immediate:
                            Immediate();
                            switch (rate)
                            {
                                //qualified immediate fixed annuity
                                case AnnuityRate.Fixed:
                                    Fixed();
                                    break;
                                //qualified immediate variable annuity
                                case AnnuityRate.Variable:
                                    Variable();
                                    break;
                            }
                            break;
                    }                 
                    break;
            }
            CalculateData(tax, stock);
        }
    }

    public enum AnnuityTax
    {
        Nonqualified,
        Qualified
    }
    public enum AnnuityTime
    {
        Immediate,
        Deferred
    }
    public enum AnnuityRate
    {
        Variable,
        Fixed
    }
}
