using System.Security.Cryptography.X509Certificates;

namespace Challenge.Core
{
    public class CertificateManager
    {
        private readonly X509Store store;

        public enum ResponseType
        {
            Success,
            InvalidCertificate,
            AlreadyExists,
            NotPresent
        }

        public CertificateManager()
        {
            store = new X509Store("MY", StoreLocation.CurrentUser);
        }

        public ResponseType AddCertificate(byte[] data)
        {
            ResponseType result;

            store.Open(OpenFlags.ReadWrite);

            X509Certificate2 cert = LoadCertificate(data);

            if(cert != null)
            {
                if (StoreHasCertificate(cert))
                {
                    result = ResponseType.AlreadyExists;
                }
                else
                {
                    store.Add(cert);
                    result = ResponseType.Success;
                }
            }
            else
            {
                result = ResponseType.InvalidCertificate;
            }

            store.Close();
            return result;
        }

        public ResponseType RemoveCertificate(byte[] data)
        {
            ResponseType result;

            store.Open(OpenFlags.ReadWrite);

            X509Certificate2 cert = LoadCertificate(data);

            if (cert != null)
            {
                if (StoreHasCertificate(cert))
                {
                    store.Remove(cert);
                    result = ResponseType.Success;
                }
                else
                {
                    result = ResponseType.NotPresent;
                }
            }
            else
            {
                result = ResponseType.InvalidCertificate;
            }

            store.Close();
            return result;
        }

        private bool StoreHasCertificate(X509Certificate2 cert)
        {
            X509Certificate2Collection storeCollection = (X509Certificate2Collection)store.Certificates;
            return storeCollection.Contains(cert);
        }

        private X509Certificate2 LoadCertificate(byte[] data)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2();
                cert.Import(data);
                return cert;
            }
            catch
            {
                return null;
            }
        }

    }
}
