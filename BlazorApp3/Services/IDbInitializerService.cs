namespace BlazerApp3.Services
{
    public interface IDbInitializerService
    {
       

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        void SeedData();
    }
}
