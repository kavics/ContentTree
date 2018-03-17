using System;
using ContentTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SenseNet.Tools.CommandLineArguments;

namespace Tests
{
    [TestClass]
    public class SettingsTests
    {
        [TestMethod]
        public void Settings_Site_Missing()
        {
            var args = new string[0];
            var settings = new Settings();
            var state = ResultState.Succesful;
            string errorMessage = null;

            try
            {
                ArgumentParser.Parse(args, settings);
                Assert.Fail("Exception was not thrown.");
            }
            catch (ParsingException e)
            {
                state = e.ErrorCode;
                errorMessage = e.FormattedMessage;
            }

            Assert.AreEqual(ResultState.MissingArgument, state);
            Assert.AreEqual("Missing argument: Site", errorMessage);
        }
        [TestMethod]
        public void Settings_Site_0()
        {
            var args = new[] { "-s:\"\"" };
            var settings = new Settings();
            ArgumentException argEx = null;

            try
            {
                ArgumentParser.Parse(args, settings);
                Assert.Fail("Exception was not thrown.");
            }
            catch (Exception e)
            {
                if (!(e is ArgumentException))
                    e = e.InnerException;
                argEx = e as ArgumentException;
            }

            Assert.IsNotNull(argEx);
            Assert.AreEqual("Site", argEx.ParamName);
        }
        [TestMethod]
        public void Settings_Site_1()
        {
            var args = new[] { "-s:url1" };
            var settings = new Settings();

            var result = ArgumentParser.Parse(args, settings);
            Assert.IsFalse(result.IsHelp);
            Assert.AreEqual(1, settings.SiteUrls.Length);
            Assert.AreEqual("url1", settings.SiteUrls[0]);
        }
        [TestMethod]
        public void Settings_Site_3()
        {
            var args = new[] { "-s:url1;url2;url3" };
            var settings = new Settings();

            var result = ArgumentParser.Parse(args, settings);
            Assert.IsFalse(result.IsHelp);
            Assert.AreEqual(3, settings.SiteUrls.Length);
            Assert.AreEqual("url1", settings.SiteUrls[0]);
            Assert.AreEqual("url2", settings.SiteUrls[1]);
            Assert.AreEqual("url3", settings.SiteUrls[2]);
        }

        [TestMethod]
        public void Settings_Indent()
        {
            var args = new[] { "-s:url1", "-i:---" };
            var settings = new Settings();

            var result = ArgumentParser.Parse(args, settings);
            Assert.IsFalse(result.IsHelp);
            Assert.AreEqual("---", settings.Indent);
        }
    }
}
