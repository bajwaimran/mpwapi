using System;
using System.Threading;

using DevExpress.Data.Filtering;

using DevExpress.Xpo;
using DevExpress.Xpo.DB.Exceptions;
using DevExpress.Xpo.Metadata;
using BestellErfassung.DomainObjects.Tools;


namespace BestellErfassung.FileHelpers
{
    public static class IntegerFunctions
    {
        #region NumberRanges
        #region private constants number ranges
        private const int maxIdGenerationAttempts = 12;
        private const int minConflictDelay = 50;    // milliseconds
        private const int maxConflictDelay = 500;   // milliseconds
        #endregion

        #region ProceedeNumber
        internal static int ProceedeNumber(string name, int number, int startNumber)
        {
            // Create criteria to get object
            BinaryOperator binOpType = new BinaryOperator("Nummerart", name, BinaryOperatorType.Equal);
            CriteriaOperator criteria = CriteriaOperator.Parse(String.Format("{0}", binOpType));

            // If no number given by user, proceed with automatic - else set given default number
            if (number == 0)
            {
                return IntegerFunctions.GetNextNumber(name, startNumber, criteria);
            }
            else
            {
                return IntegerFunctions.SetNewNumber(name, number, criteria);
            }
        }
        #endregion

        #region GetNextNumber
        private static int GetNextNumber(string name, int startNumber, CriteriaOperator criteria)
        {
            int attempt = 1;
            while (true)
            {
                try
                {
                    int returnNumber = 0;
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        // Try to find appropriate object, else create a new one
                        DomainObjects.Tools.Nummernvergabe numberRange = uow.FindObject<DomainObjects.Tools.Nummernvergabe>(criteria, false);
                        if (numberRange != null)
                        {
                            // Do processing
                            returnNumber = numberRange.Nummer;
                            numberRange.Nummer = returnNumber + 1;
                        }
                        else
                        {
                            DomainObjects.Tools.Nummernvergabe newNumberRange = new DomainObjects.Tools.Nummernvergabe(uow);
                            newNumberRange.Nummerart = name;
                            newNumberRange.Nummer = startNumber + 1;
                            returnNumber = startNumber;
                        }

                        // Save changes if any
                        if (uow.InTransaction)
                            uow.CommitChanges();
                    }
                    return returnNumber;
                }
                catch (LockingException ex)
                {
                    if (attempt >= maxIdGenerationAttempts)
                        throw new Exception();
                }

                if (attempt > maxIdGenerationAttempts / 2)
                    Thread.Sleep(new Random().Next(minConflictDelay, maxConflictDelay));

                attempt++;
            }
        }
        #endregion

        #region SetNewNumber
        private static int SetNewNumber(string name, int number, CriteriaOperator criteria)
        {
            int attempt = 1;
            while (true)
            {
                try
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        // Try to find appropriate object, else create a new one
                        DomainObjects.Tools.Nummernvergabe numberRange = uow.FindObject<DomainObjects.Tools.Nummernvergabe>(criteria, false);
                        if (numberRange != null)
                        {
                            numberRange.Nummer = number + 1;
                        }
                        else
                        {
                            DomainObjects.Tools.Nummernvergabe newNumberRange = new DomainObjects.Tools.Nummernvergabe(uow);
                            newNumberRange.Nummerart = name;
                            newNumberRange.Nummer = number + 1;
                        }

                        // Save changes if any
                        if (uow.InTransaction)
                            uow.CommitChanges();
                    }
                    return 0;
                }
                catch (LockingException ex)
                {
                    if (attempt >= maxIdGenerationAttempts)
                        throw new Exception();
                }

                if (attempt > maxIdGenerationAttempts / 2)
                    Thread.Sleep(new Random().Next(minConflictDelay, maxConflictDelay));

                attempt++;
            }
        }
        #endregion
        #endregion

        //public class SequenceGenerator<T> : IDisposable
        //{
        //    private ExplicitUnitOfWork euow;
        //    private XPClassInfo classInfo;
        //    private Sequence seq;
        //    public SequenceGenerator(IDataLayer dataLayer)
        //    {
        //        euow = new ExplicitUnitOfWork(dataLayer);
        //        classInfo = euow.GetClassInfo<T>();
        //    }
        //    public void Accept()
        //    {
        //        euow.CommitChanges();
        //    }
        //    public long GetNextId()
        //    {
        //        long nextId;
        //        while (true)
        //        {
        //            try
        //            {
        //                if (seq == null)
        //                {
        //                    seq = euow.GetObjectByKey<Sequence>(classInfo.FullName, true);
        //                    if (seq == null)
        //                    {
        //                        seq = new Sequence(euow);
        //                        seq.TypeName = classInfo.FullName;
        //                        seq.NextId = 0;
        //                    }
        //                }
        //                nextId = seq.NextId;
        //                seq.NextId++;
        //                euow.FlushChanges();
        //            }
        //            catch (LockingException)
        //            {
        //                seq = null;
        //                continue;
        //            }
        //            break;
        //        }
        //        return nextId;
        //    }
        //    public void Close()
        //    {
        //        if (euow != null)
        //            euow.Dispose();
        //    }
        //    void IDisposable.Dispose()
        //    {
        //        Close();
        //    }
        //}



    }
}
