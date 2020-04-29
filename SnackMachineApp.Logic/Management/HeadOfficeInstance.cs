using SnackMachineApp.Logic.Utils;
using System;

namespace SnackMachineApp.Logic.Management
{
    public static class HeadOfficeInstance
    {
        public static readonly long HeadOfficeId = 1;

        private static HeadOffice GetDefault()
        {
            return ObjectFactory.Instance.Resolve<IHeadOfficeRepository>().GetById(HeadOfficeId);
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
