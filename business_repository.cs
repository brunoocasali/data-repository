using System.Data;

/// <summary> BusinessRepository v0.3.1.0,
/// <para></para>
/// Made by Bruno Casali at Tuesday, June 25, 2014 :/
/// <!--
/// You can contact me by: brunoocasali@gmail.com / https://about.me/brunocasali
/// -->
/// </summary>

namespace DataRepository
{
    public abstract class BusinessRepository<T>
    {
        public abstract bool Add();
        public abstract bool Update();
        public abstract bool Delete();
        /// <summary>
        /// Each class, iplements the call of DataRepository.Total("table_name");
        /// </summary>
        /// <returns>How many rows that table has.</returns>
        public abstract int Total();
        /// <summary>
        /// This is a constant to be reused on selects statements
        /// </summary>
        protected abstract string Fields { get; }
        protected abstract T FillAttributes(DataRow dr);
        public abstract T Select(int id);
    }

    public abstract class BusinessRepositoryBase<T> : BusinessRepository<T>
    {
        protected abstract void FillAttributes(DataRow dr, T obj);
        public abstract int Total(string search);
    }
}
