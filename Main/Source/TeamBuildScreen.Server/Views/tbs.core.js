/*
* Team Build Screen 2.0.0
* http://teambuildscreen.codeplex.com
*
* Copyright (c) 2011 Jim Liddell. All rights reserved.
*/

var TeamBuildScreen = TeamBuildScreen || {};

TeamBuildScreen.ConvertStatusToString = function (status) {
    if (status === "InProgress") {
        return "In Progress";
    } else if (status === "NotStarted") {
        return "Not Started"
    } else if (status === "PartiallySucceeded") {
        return "Partially Succeeded"
    } else if (status === "Loading") {
        return "Loading..."
    } else if (status === "NoneFound") {
        return "No build(s) found."
    } else {
        return status;
    }
}
TeamBuildScreen.Log = function(s) {
    if (typeof console != "undefined") {
        console.log(s);
    }
}

TeamBuildScreen.BuildServer = function (builds, interval, messageView) {
    this.init(builds, interval, messageView);
}
TeamBuildScreen.BuildServer.prototype.init = function (builds, interval, messageView) {
    this.listeners = [];
    this.builds = builds;

    var self = this;
    var timer;

    function query() {
        $.getJSON('/builds', function (data) {
            self.builds = data;
            self.update();
            messageView.hide();
            timer = setTimeout(query, interval);
        }).error(function (response) {
            messageView.error();
            TeamBuildScreen.Log(response.responseText)
            timer = setTimeout(query, interval);
        });
    }

    this.start = function () {
        query();
    }

    this.stop = function () {
        clearTimeout(timer);
    }

    this.start();
}
TeamBuildScreen.BuildServer.prototype.register = function (callback) {
	if(typeof callback === "function" || typeof callback === "object") {
		if(typeof callback.update === "function") {
			this.listeners.push(callback);
		}
	}
}
TeamBuildScreen.BuildServer.prototype.update = function () {
	var size = this.listeners.length;

	for(var x=0; x < size; x++) {
		this.listeners[x].update(this);
	}
}
TeamBuildScreen.BuildServer.prototype.getBuild = function (description) {
	for (i = 0; i < this.builds.length; i++)
	{
		var build = this.builds[i];

		if (build.description == description) {
			return build;
		}
	}
}

TeamBuildScreen.MessageView = function (id) {
    this.init(id);
}
TeamBuildScreen.MessageView.prototype.init = function (id) {
    this.$view = $("#" + id);
    this.$view.fadeTo(0, 0);
}
TeamBuildScreen.MessageView.prototype.error = function () {
    this.setMessage("Error contacting build server.");
    this.show();
}
TeamBuildScreen.MessageView.prototype.show = function () {
    this.$view.fadeTo(1000, 0.85);
}
TeamBuildScreen.MessageView.prototype.hide = function () {
    this.setMessage("");
    this.$view.fadeTo(1000, 0);
}
TeamBuildScreen.MessageView.prototype.setMessage = function (message) {
    this.$view.find("span").html(message);
}