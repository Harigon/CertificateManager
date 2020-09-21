using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Challenge.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Challenge.Core.CertificateManager;
using FluentAssertions;

namespace Challenge.Tests
{
    [TestClass]
    public class CertificateManagerTests
    {

        [TestMethod]
        [DeploymentItem("TestData\\test.cer")]
        public void CertificateManager_AddCertificiate_Success()
        {
            // Assemble
            byte[] cert = File.ReadAllBytes("test.cer");
            CertificateManager manager = new CertificateManager();
            manager.RemoveCertificate(cert);//If its already there

            // Action
            ResponseType result = manager.AddCertificate(cert);

            // Assert
            result.Should().Be(ResponseType.Success);
        }

        [TestMethod]
        [DeploymentItem("TestData\\test.cer")]
        public void CertificateManager_AddCertificiate_AlreadyExists()
        {
            // Assemble
            byte[] cert = File.ReadAllBytes("test.cer");
            CertificateManager manager = new CertificateManager();
            manager.AddCertificate(cert);//Make sure its there already

            // Action
            ResponseType result = manager.AddCertificate(cert);

            // Assert
            result.Should().Be(ResponseType.AlreadyExists);
        }

        [TestMethod]
        [DeploymentItem("TestData\\invalid.txt")]
        public void CertificateManager_AddCertificiate_Invalid()
        {
            // Assemble
            byte[] cert = File.ReadAllBytes("invalid.txt");
            CertificateManager manager = new CertificateManager();

            // Action
            ResponseType result = manager.AddCertificate(cert);

            // Assert
            result.Should().Be(ResponseType.InvalidCertificate);
        }

        [TestMethod]
        [DeploymentItem("TestData\\test.cer")]
        public void CertificateManager_RemoveCertificiate_Success()
        {
            // Assemble
            byte[] cert = File.ReadAllBytes("test.cer");
            CertificateManager manager = new CertificateManager();
            manager.AddCertificate(cert);//Make sure its there already

            // Action
            ResponseType result = manager.RemoveCertificate(cert);

            // Assert
            result.Should().Be(ResponseType.Success);
        }

        [TestMethod]
        [DeploymentItem("TestData\\test.cer")]
        public void CertificateManager_RemoveCertificiate_NotPresent()
        {
            // Assemble
            byte[] cert = File.ReadAllBytes("test.cer");
            CertificateManager manager = new CertificateManager();
            manager.RemoveCertificate(cert);//If its already there

            // Action
            ResponseType result = manager.RemoveCertificate(cert);

            // Assert
            result.Should().Be(ResponseType.NotPresent);
        }

        [TestMethod]
        [DeploymentItem("TestData\\invalid.txt")]
        public void CertificateManager_RemoveCertificiate_Invalid()
        {
            // Assemble
            byte[] cert = File.ReadAllBytes("invalid.txt");
            CertificateManager manager = new CertificateManager();

            // Action
            ResponseType result = manager.RemoveCertificate(cert);

            // Assert
            result.Should().Be(ResponseType.InvalidCertificate);
        }




    }
}
