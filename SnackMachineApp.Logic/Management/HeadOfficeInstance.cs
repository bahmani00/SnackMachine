using System;

namespace SnackMachineApp.Logic.Management
{
    public static class HeadOfficeInstance
    {
        private const long HeadOfficeId = 1;

        private static HeadOffice GetDefault()
        {
            var repository = new HeadOfficeRepository();
            return repository.Get(HeadOfficeId);
        }

        #region Singleton
        private static readonly Lazy<HeadOffice> instance = new Lazy<HeadOffice>(() => GetDefault());


        public static HeadOffice Instance
        {
            get
            {
                return instance.Value;
            }
        }

        #endregion Singleton
    }
}
