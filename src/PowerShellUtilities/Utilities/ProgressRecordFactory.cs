using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;

namespace PowerShellUtilities.Utilities
{
    internal static class ProgressRecordFactory
    {
        public static ProgressRecord Progress()
        {
            ProgressRecord record = new ProgressRecord(1, string.Empty, string.Empty);
            record.RecordType = ProgressRecordType.Processing;
            return record;
        }

        public static ProgressRecord Progress(int activityId, string activity)
        {
            ProgressRecord record = new ProgressRecord(activityId, activity, activity);
            record.RecordType = ProgressRecordType.Processing;
            return record;
        }

        public static ProgressRecord Progress(int activityId, string activity, string status)
        {
            ProgressRecord record = new ProgressRecord(activityId, activity, status);
            record.RecordType = ProgressRecordType.Processing;
            return record;
        }

        public static ProgressRecord Progress(int activityId, string activity, int percentComplete)
        {
            ProgressRecord record = new ProgressRecord(activityId, activity, activity);
            record.RecordType = ProgressRecordType.Processing;
            record.PercentComplete = percentComplete;
            return record;
        }

        public static ProgressRecord Progress(int activityId, string activity, string status, int percentComplete)
        {
            ProgressRecord record = new ProgressRecord(activityId, activity, status);
            record.RecordType = ProgressRecordType.Processing;
            record.PercentComplete = percentComplete;
            return record;
        }

        public static ProgressRecord Progress(int activityId, string activity, string status, string currentOperation)
        {
            ProgressRecord record = new ProgressRecord(activityId, activity, status);
            record.RecordType = ProgressRecordType.Processing;
            record.CurrentOperation = currentOperation;
            return record;
        }

        public static ProgressRecord Progress(int activityId, string activity, string status, string currentOperation, int percentComplete)
        {
            ProgressRecord record = new ProgressRecord(activityId, activity, status);
            record.RecordType = ProgressRecordType.Processing;
            record.CurrentOperation = currentOperation;
            record.PercentComplete = percentComplete;
            return record;
        }


        public static ProgressRecord Progress(string activity)
        {
            ProgressRecord record = new ProgressRecord(1, activity, activity);
            record.RecordType = ProgressRecordType.Processing;
            return record;
        }

        public static ProgressRecord Progress(string activity, string status)
        {
            ProgressRecord record = new ProgressRecord(1, activity, status);
            record.RecordType = ProgressRecordType.Processing;
            return record;
        }

        public static ProgressRecord Progress(string activity, int percentComplete)
        {
            ProgressRecord record = new ProgressRecord(1, activity, activity);
            record.RecordType = ProgressRecordType.Processing;
            record.PercentComplete = percentComplete;
            return record;
        }

        public static ProgressRecord Progress(string activity, string status, int percentComplete)
        {
            ProgressRecord record = new ProgressRecord(1, activity, status);
            record.RecordType = ProgressRecordType.Processing;
            record.PercentComplete = percentComplete;
            return record;
        }

        public static ProgressRecord Progress(string activity, string status, string currentOperation)
        {
            ProgressRecord record = new ProgressRecord(1, activity, status);
            record.RecordType = ProgressRecordType.Processing;
            record.CurrentOperation = currentOperation;
            return record;
        }

        public static ProgressRecord Progress(string activity, string status, string currentOperation, int percentComplete)
        {
            ProgressRecord record = new ProgressRecord(1, activity, status);
            record.RecordType = ProgressRecordType.Processing;
            record.CurrentOperation = currentOperation;
            record.PercentComplete = percentComplete;
            return record;
        }
    }
}
