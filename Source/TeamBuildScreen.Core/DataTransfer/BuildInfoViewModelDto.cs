using System;
using System.Runtime.Serialization;

namespace TeamBuildScreen.Core.DataTransfer
{
    [DataContract]
    public class BuildInfoViewModelDto
    {
        /// <summary>
        /// Gets or sets the status for the <see cref="BuildInfoViewModelDto"/>.
        /// </summary>
        [DataMember(Name="status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the description for the <see cref="BuildInfoViewModelDto"/>.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who requested the <see cref="BuildInfoViewModelDto"/>.
        /// </summary>
        [DataMember(Name = "requestedBy")]
        public string RequestedBy { get; set; }

        /// <summary>
        /// Gets or sets the time when the build was started.
        /// </summary>
        [DataMember(Name = "startedOn")]
        public string StartedOn { get; set; }

        /// <summary>
        /// Gets or sets the time the build was completed.
        /// </summary>
        [DataMember(Name = "completedOn")]
        public string CompletedOn { get; set; }

        /// <summary>
        /// Gets or sets the test results for the <see cref="BuildInfoViewModelDto"/>.
        /// </summary>
        [DataMember(Name = "testResults")]
        public string TestResults { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the build has any builds queued.
        /// </summary>
        [DataMember(Name = "isQueued")]
        public bool IsQueued { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current build was completed more than a week ago.
        /// </summary>
        [DataMember(Name = "isStale")]
        public bool IsStale { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current build is finished.
        /// </summary>
        [DataMember(Name = "isFinished")]
        public bool IsFinished { get; set; }

        /// <summary>
        /// Gets or sets a value between 0 and 1 indicating the progress of the build.
        /// </summary>
        [DataMember(Name = "progress")]
        public decimal Progress { get; set; }
    }
}