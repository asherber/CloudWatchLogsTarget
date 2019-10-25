﻿using NLog.Targets.CloudWatchLogs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace NLog.Targets.CloudWatchLogs.Tests
{
    public class CloudWatchLogsTargetTests
    {
        private class CrackedTarget: CloudWatchLogsTarget
        {
            public new LogDatum CreateDatum(LogEventInfo logInfo) => base.CreateDatum(logInfo);            
        }

        private static readonly LogEventInfo _logEventInfo = new LogEventInfo();

        [Fact]
        public void Target_With_No_Funcs_Should_Have_Default_Names()
        {
            var target = new CrackedTarget();
            string expectedGroupName = "unspecified", expectedStreamName = "unspecified";

            var datum = target.CreateDatum(_logEventInfo);

            Assert.Equal(datum.GroupName, expectedGroupName);
            Assert.Equal(datum.StreamName, expectedStreamName);
        }

        [Fact]
        public void Group_Name_Func_Should_Override_Default()
        {
            var expectedGroupName = Guid.NewGuid().ToString();
            var target = new CrackedTarget()
            {
                LogGroupNameFunc = () => expectedGroupName
            };

            var datum = target.CreateDatum(_logEventInfo);

            Assert.Equal(datum.GroupName, expectedGroupName);
        }

        [Fact]
        public void Stream_Name_Func_Should_Override_Default()
        {
            var expectedStreamName = Guid.NewGuid().ToString();
            var target = new CrackedTarget()
            {
                LogStreamNameFunc = () => expectedStreamName
            };

            var datum = target.CreateDatum(_logEventInfo);

            Assert.Equal(datum.StreamName, expectedStreamName);
        }
    }
}
