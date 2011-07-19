/*
* Team Build Screen 2.0.0
* http://teambuildscreen.codeplex.com
*
* Copyright (c) 2011 Jim Liddell. All rights reserved.
*/

var TeamBuildScreen = TeamBuildScreen || {};

TeamBuildScreen.BuildLabel = function (paper, x, y, text, fontSize, width) {
	this.init(paper, x, y, text, fontSize, width);
};
TeamBuildScreen.BuildLabel.prototype.init = function (paper, x, y, text, fontSize, width) {
	var attributes = {"font-family": "Segoe UI", "text-anchor": "start"}
	this.innerLabel = paper.text(x, y, text).attr(attributes).attr({"font-size": fontSize});
	this.width = width;
	this.text = text;
	this.setText(text);
}
TeamBuildScreen.BuildLabel.prototype.setText = function (text) {
	this.text = text;
	this.innerLabel.attr("text", this.text);

	if (this.innerLabel.getBBox().width > this.width) {
		var original = this.innerLabel.attr("text");
		var drop = 1;

		while (this.innerLabel.getBBox().width > this.width) {
			var truncated = original.substr(0, original.length - drop) + "...";

			this.innerLabel.attr("text", truncated);

			drop++;
		}
	}
};
TeamBuildScreen.BuildLabel.prototype.remove = function () {
	this.innerLabel.remove();
}
TeamBuildScreen.BuildIcon = function (paper, status) {
	this.init(paper, status);
};
TeamBuildScreen.BuildIcon.prototype.init = function (paper, status) {
	if (this.status == undefined || this.status != status) {
		this.createIconFromStatus(paper, status);
	}

	this.nativeHeight = this.getWidth();
	this.status = status;
}
TeamBuildScreen.BuildIcon.prototype.createIconFromStatus = function (paper, status) {
	if (status == "InProgress") {
		this.icon = this.createInProgressIcon(paper);
	} else if (status == "NotStarted") {
		this.icon = this.createNotStartedIcon(paper);
	} else if (status == "Failed") {
		this.icon = this.createFailedIcon(paper);
	} else if (status == "Stopped") {
		this.icon = this.createStoppedIcon(paper);
	} else if (status == "Succeeded") {
		this.icon = this.createSuccessIcon(paper);
    } else if (status == "NoneFound" || status == "Loading") {
		this.icon = this.createUnknownIcon(paper);
	} else if (status == "PartiallySucceeded") {
		this.icon = this.createPartiallySucceededIcon(paper);
	}
}
TeamBuildScreen.BuildIcon.prototype.toFront = function () {
	this.icon.toFront();
}
TeamBuildScreen.BuildIcon.prototype.createPartiallySucceededIcon = function (paper) {
	var partiallySucceededIcon = paper.set();
	var offset = 150;
	var offset2 = 20;

	partiallySucceededIcon.push(
		paper.ellipse(203, 203, 203, 203).attr({"opacity": 0, "stroke" : 0}),

		paper.ellipse(128, 128, 128, 128).attr({fill : "315-#CF2C00-#7B1A00", "opacity" : 1, "stroke" : 0}).translate(offset, offset),
		paper.ellipse(128, 128, 116.537, 116.537).attr({fill : "135-#CF2C00-#FFEBD2", "opacity" : 1, "stroke" : 0}).translate(offset, offset),
		//paper.ellipse(128, 128, 116.537, 116.537).attr({fill : "r(0.5, 0.5)rgba(207,44,0,0.796875)-rgba(207,44,0,0.3984375):55-rgba(207,44,0,0)", "opacity" : 0, "stroke" : 0}).translate(offset, offset),
		paper.path("F1 M 45.5957,45.5956C 0.0850375,91.1062 0.0850306,164.894 45.5957,210.404C 49.2588,214.067 41.8508,207.076 45.8508,210.149C 10.7398,164.453 25.3623,109.057 67.2098,67.2097C 109.057,25.3622 164.453,10.7397 210.149,45.8507C 207.076,41.8508 214.067,49.2587 210.404,45.5956C 164.894,0.0849457 91.1063,0.0849457 45.5957,45.5956 Z ").attr({"fill" : "#FFFFFF", "opacity" : 0.2, "stroke" : 0}).translate(offset, offset),
		paper.path("F1 M 210.404,210.404C 255.915,164.894 255.915,91.1065 210.404,45.5959C 206.741,41.9328 214.297,49.072 210.297,45.9986C 245.408,91.6947 230.638,146.943 188.79,188.79C 146.943,230.638 91.6946,245.408 45.9985,210.297C 49.0719,214.297 41.9327,206.741 45.5958,210.404C 91.1064,255.915 164.894,255.915 210.404,210.404 Z ").attr({fill : "#CF2C00", "opacity" : 0.2, "stroke" : 0}).translate(offset, offset),
		paper.path("M 143.284,125.453L 178.627,78.3283L 180.06,76.4179L 158.09,76.4179L 128,116.537L 97.9104,76.4179L 75.9403,76.4179L 77.3731,78.3284L 112.716,125.453L 112.716,136.915L 73.5524,189.134L 95.5225,189.134L 128,145.831L 160.478,189.134L 182.448,189.134L 143.284,136.915L 143.284,125.453 Z ").attr({fill : "#FFFFFF", "stroke" : 0}).translate(offset, offset),
		paper.path("M 97.9106,76.4179L 75.9404,76.4179L 68.7762,66.8657L 99.3434,66.8657L 128,105.075L 156.657,66.8656L 187.224,66.8656L 180.06,76.4179L 158.09,76.4179L 128,116.537L 97.9106,76.4179 Z ").attr({fill : "#7B1A00", "opacity" : 1, "stroke" : 0}).translate(offset, offset),
		paper.path("M 182.448,189.134L 143.284,136.915L 143.284,125.453L 147.582,131.184L 191.045,189.134L 182.448,189.134 Z ").attr({fill : "#7B1A00", "stroke" : 0}).translate(offset, offset),
		paper.path("M 112.716,125.453L 112.717,136.915L 73.5524,189.134L 64.9553,189.134L 108.418,131.184L 112.716,125.453 Z ").attr({fill : "#7B1A00", "stroke" : 0}).translate(offset, offset),

		paper.ellipse(128, 128, 128, 128).attr({fill : "315-#2B7720-#125400", "opacity": 1, "stroke" : 0}).translate(offset2, offset2),
		paper.ellipse(128, 128, 116.537, 116.537).attr({fill : "135-#2B7720-#D2FDC6", "opacity" : 1, "stroke" : 0}).translate(offset2, offset2),
		//paper.ellipse(128, 128, 116.537, 116.537).attr({fill : "r(0.5, 0.5)rgba(46,121,35,0.796875)-rgba(54,127,42,0.3984375):55-rgba(62,133,50,0)", "opacity" : 0, "stroke" : 0}).translate(offset2, offset2),
		paper.path("F1 M 45.5957,45.5956C 0.0850375,91.1062 0.0850306,164.894 45.5957,210.404C 49.2588,214.067 41.8508,207.076 45.8508,210.149C 10.7398,164.453 25.3623,109.057 67.2098,67.2097C 109.057,25.3622 164.453,10.7397 210.149,45.8507C 207.076,41.8508 214.067,49.2587 210.404,45.5956C 164.894,0.0849457 91.1063,0.0849457 45.5957,45.5956 Z ").attr({"fill" : "#FFFFFF", "opacity" : 0.2, "stroke" : 0}).translate(offset2, offset2),
		paper.path("F1 M 210.404,210.404C 255.915,164.894 255.915,91.1065 210.404,45.5959C 206.741,41.9328 214.297,49.072 210.297,45.9986C 245.408,91.6947 230.638,146.943 188.79,188.79C 146.943,230.638 91.6946,245.408 45.9985,210.297C 49.0719,214.297 41.9327,206.741 45.5958,210.404C 91.1064,255.915 164.894,255.915 210.404,210.404 Z ").attr({fill : "#125400", "opacity" : 0.2, "stroke" : 0}).translate(offset2, offset2),
		paper.path("M 177.982,71.2619L 107.736,141.508L 78.0166,111.789L 56.4023,133.403L 61.8059,138.807L 78.0166,122.596L 107.736,152.315L 177.982,82.0691L 194.193,98.2798L 199.597,92.8761L 177.982,71.2619 Z ").attr({fill : "#125400", "stroke" : 0}).translate(offset2, offset2),
		paper.path("M 177.982,82.069L 107.736,152.315L 78.0166,122.596L 61.8059,138.807L 107.736,184.737L 194.193,98.2798L 177.982,82.069 Z ").attr({fill : "#FFFFFF", "stroke" : 0}).translate(offset2, offset2)
	);

	return partiallySucceededIcon;
}
TeamBuildScreen.BuildIcon.prototype.createSuccessIcon = function (paper) {
	var successIcon = paper.set();

	successIcon.push(
		paper.ellipse(128, 128, 128, 128).attr({fill : "315-#2B7720-#125400", "opacity": 1, "stroke" : 0}),
		paper.ellipse(128, 128, 116.537, 116.537).attr({fill : "135-#2B7720-#D2FDC6", "opacity" : 1, "stroke" : 0}),
		//paper.ellipse(128, 128, 116.537, 116.537).attr({fill : "r(0.5, 0.5)rgba(46,121,35,0.796875)-rgba(54,127,42,0.3984375):55-rgba(62,133,50,0)", "opacity" : 0, "stroke" : 0}),
		paper.path("F1 M 45.5957,45.5956C 0.0850375,91.1062 0.0850306,164.894 45.5957,210.404C 49.2588,214.067 41.8508,207.076 45.8508,210.149C 10.7398,164.453 25.3623,109.057 67.2098,67.2097C 109.057,25.3622 164.453,10.7397 210.149,45.8507C 207.076,41.8508 214.067,49.2587 210.404,45.5956C 164.894,0.0849457 91.1063,0.0849457 45.5957,45.5956 Z ").attr({"fill" : "#FFFFFF", "opacity" : 0.2, "stroke" : 0}),
		paper.path("F1 M 210.404,210.404C 255.915,164.894 255.915,91.1065 210.404,45.5959C 206.741,41.9328 214.297,49.072 210.297,45.9986C 245.408,91.6947 230.638,146.943 188.79,188.79C 146.943,230.638 91.6946,245.408 45.9985,210.297C 49.0719,214.297 41.9327,206.741 45.5958,210.404C 91.1064,255.915 164.894,255.915 210.404,210.404 Z ").attr({fill : "#125400", "opacity" : 0.2, "stroke" : 0}),
		paper.path("M 177.982,71.2619L 107.736,141.508L 78.0166,111.789L 56.4023,133.403L 61.8059,138.807L 78.0166,122.596L 107.736,152.315L 177.982,82.0691L 194.193,98.2798L 199.597,92.8761L 177.982,71.2619 Z ").attr({fill : "#125400", "stroke" : 0}),
		paper.path("M 177.982,82.069L 107.736,152.315L 78.0166,122.596L 61.8059,138.807L 107.736,184.737L 194.193,98.2798L 177.982,82.069 Z ").attr({fill : "#FFFFFF", "stroke" : 0})
	);

	return successIcon;
}
TeamBuildScreen.BuildIcon.prototype.createFailedIcon = function (paper) {
	var failedIcon = paper.set();

	failedIcon.push(
		paper.ellipse(128, 128, 128, 128).attr({fill : "315-#CF2C00-#7B1A00", "opacity" : 1, "stroke" : 0}),
		paper.ellipse(128, 128, 116.537, 116.537).attr({fill : "135-#CF2C00-#FFEBD2", "opacity" : 1, "stroke" : 0}),
		//paper.ellipse(128, 128, 116.537, 116.537).attr({fill : "r(0.5, 0.5)rgba(207,44,0,0.796875)-rgba(207,44,0,0.3984375):55-rgba(207,44,0,0)", "opacity" : 0, "stroke" : 0}),
		paper.path("F1 M 45.5957,45.5956C 0.0850375,91.1062 0.0850306,164.894 45.5957,210.404C 49.2588,214.067 41.8508,207.076 45.8508,210.149C 10.7398,164.453 25.3623,109.057 67.2098,67.2097C 109.057,25.3622 164.453,10.7397 210.149,45.8507C 207.076,41.8508 214.067,49.2587 210.404,45.5956C 164.894,0.0849457 91.1063,0.0849457 45.5957,45.5956 Z ").attr({"fill" : "#FFFFFF", "opacity" : 0.2, "stroke" : 0}),
		paper.path("F1 M 210.404,210.404C 255.915,164.894 255.915,91.1065 210.404,45.5959C 206.741,41.9328 214.297,49.072 210.297,45.9986C 245.408,91.6947 230.638,146.943 188.79,188.79C 146.943,230.638 91.6946,245.408 45.9985,210.297C 49.0719,214.297 41.9327,206.741 45.5958,210.404C 91.1064,255.915 164.894,255.915 210.404,210.404 Z ").attr({fill : "#CF2C00", "opacity" : 0.2, "stroke" : 0}),
		paper.path("M 143.284,125.453L 178.627,78.3283L 180.06,76.4179L 158.09,76.4179L 128,116.537L 97.9104,76.4179L 75.9403,76.4179L 77.3731,78.3284L 112.716,125.453L 112.716,136.915L 73.5524,189.134L 95.5225,189.134L 128,145.831L 160.478,189.134L 182.448,189.134L 143.284,136.915L 143.284,125.453 Z ").attr({fill : "#FFFFFF", "stroke" : 0}),
		paper.path("M 97.9106,76.4179L 75.9404,76.4179L 68.7762,66.8657L 99.3434,66.8657L 128,105.075L 156.657,66.8656L 187.224,66.8656L 180.06,76.4179L 158.09,76.4179L 128,116.537L 97.9106,76.4179 Z ").attr({fill : "#7B1A00", "opacity" : 1, "stroke" : 0}),
		paper.path("M 182.448,189.134L 143.284,136.915L 143.284,125.453L 147.582,131.184L 191.045,189.134L 182.448,189.134 Z ").attr({fill : "#7B1A00", "stroke" : 0}),
		paper.path("M 112.716,125.453L 112.717,136.915L 73.5524,189.134L 64.9553,189.134L 108.418,131.184L 112.716,125.453 Z ").attr({fill : "#7B1A00", "stroke" : 0})
	);

	return failedIcon;
}
TeamBuildScreen.BuildIcon.prototype.createStoppedIcon = function (paper) {
	var stoppedIcon = paper.set();

	stoppedIcon.push(
		paper.ellipse(128, 128, 128, 128).attr({fill : "315-#CF2C00-#7B1A00", "opacity" : 1, "stroke" : 0}),
		paper.ellipse(128, 128, 116.537, 116.537).attr({fill : "135-#717171-#B8B8B8:16-#FFFFFF", "opacity" : 1, "stroke" : 0}),
		paper.path("F1 M 45.5957,45.5956C 0.0850375,91.1062 0.0850306,164.894 45.5957,210.404C 49.2588,214.067 41.8508,207.076 45.8508,210.149C 10.7398,164.453 25.3623,109.057 67.2098,67.2097C 109.057,25.3622 164.453,10.7397 210.149,45.8507C 207.076,41.8508 214.067,49.2587 210.404,45.5956C 164.894,0.0849457 91.1063,0.0849457 45.5957,45.5956 Z ").attr({"fill" : "#FFFFFF", "opacity" : 0.2, "stroke" : 0}),
		paper.path("F1 M 210.404,210.404C 255.915,164.894 255.915,91.1065 210.404,45.5959C 206.741,41.9328 214.297,49.072 210.297,45.9986C 245.408,91.6947 230.638,146.943 188.79,188.79C 146.943,230.638 91.6946,245.408 45.9985,210.297C 49.0719,214.297 41.9327,206.741 45.5958,210.404C 91.1064,255.915 164.894,255.915 210.404,210.404 Z ").attr({fill : "#717171", "opacity" : 0.2, "stroke" : 0}),
		paper.path("M 76.4179,74.5076L 179.582,74.5076L 179.582,82.1493L 179.582,181.526L 76.4179,181.526L 76.4179,82.1493L 76.4179,74.5076 Z ").attr({fill : "#7B1A00", "stroke" : 0}),
		paper.rect(76.4179, 82.1492, 103.164, 99.3766).attr({fill : "0-#CF2C00-#7B1A00", "stroke" : 0})
	);

	return stoppedIcon;
}
TeamBuildScreen.BuildIcon.prototype.createInProgressIcon = function (paper) {
	var inProgressIcon = paper.set();

	inProgressIcon.push(
		paper.ellipse(128, 128, 128, 128).attr({fill : "315-#2B7720-#125400", "opacity": 1, "stroke" : 0}),
		paper.ellipse(128, 128, 116.537, 116.537).attr({fill : "135-#717171-#B8B8B8:16-#FFFFFF", "opacity" : 1, "stroke" : 0}),
		paper.path("M 101.254,192.955L 101.254,70.6868L 101.254,63.0448L 173.851,128L 170.03,131.821L 101.254,192.955 Z ").attr({fill : "#4A4A4A", "opacity": 1, "stroke" : 0}),
		paper.path("M 101.254,192.955L 170.03,131.821L 101.254,70.6868L 101.254,192.955 Z ").attr({fill : "0-#2B7720-#125400", "opacity": 1, "stroke" : 0}),
		paper.path("F1 M 45.5957,45.5956C 0.0850375,91.1062 0.0850306,164.894 45.5957,210.404C 49.2588,214.067 41.8508,207.076 45.8508,210.149C 10.7398,164.453 25.3623,109.057 67.2098,67.2097C 109.057,25.3622 164.453,10.7397 210.149,45.8507C 207.076,41.8508 214.067,49.2587 210.404,45.5956C 164.894,0.0849457 91.1063,0.0849457 45.5957,45.5956 Z ").attr({"fill" : "#FFFFFF", "opacity" : 0.2, "stroke" : 0}),
		paper.path("F1 M 210.404,210.404C 255.915,164.894 255.915,91.1065 210.404,45.5959C 206.741,41.9328 214.297,49.072 210.297,45.9986C 245.408,91.6947 230.638,146.943 188.79,188.79C 146.943,230.638 91.6946,245.408 45.9985,210.297C 49.0719,214.297 41.9327,206.741 45.5958,210.404C 91.1064,255.915 164.894,255.915 210.404,210.404 Z ").attr({fill : "#717171", "opacity" : 0.2, "stroke" : 0})
	);

	return inProgressIcon;
}
TeamBuildScreen.BuildIcon.prototype.createNotStartedIcon = function (paper) {
	var notStartedIcon = paper.set();

	notStartedIcon.push(
		paper.ellipse(128, 128, 128, 128).attr({fill : "315-#717171-#4A4A4A", "opacity" : 1, "stroke" : 0}),
		paper.ellipse(128, 128, 116.537, 116.537).attr({fill : "135-#717171-#B8B8B8:16-#FFFFFF", "opacity" : 1, "stroke" : 0}),
		paper.path("M 204.418,133.732L 166.209,133.732L 166.209,160.478L 128,160.478L 128,187.224L 51.5821,187.224L 51.582,129.911L 51.5821,122.269L 89.7911,122.269L 89.791,103.165L 89.791,95.5227L 128,95.5228L 128,76.4183L 128,68.7765L 204.418,68.7765L 204.418,76.4183L 204.418,133.732 Z M 101.254,129.911L 128,129.911L 128,149.015L 154.746,149.015L 154.746,106.986L 101.254,106.986L 101.254,122.269L 128,122.269L 128,129.911L 101.254,129.911L 101.254,129.911 Z M 166.209,103.165L 166.209,122.269L 192.955,122.269L 192.955,80.2393L 139.463,80.2393L 139.463,95.5229L 166.209,95.5228L 166.209,103.165 Z M 63.0447,175.762L 116.537,175.762L 116.537,133.732L 63.0447,133.732L 63.0447,175.762 Z ").attr({"fill" : "#4A4A4A", "stroke" : 0}),
		paper.path("M 204.418,133.731L 204.418,68.7762L 204.418,76.418L 128,76.418L 128,95.5224L 128,103.164L 89.7911,103.164L 89.7911,122.269L 89.7911,129.911L 51.5821,129.911L 51.5822,187.224L 128,187.224L 128,160.478L 166.209,160.478L 166.209,133.731L 204.418,133.731 Z M 101.254,122.269L 101.254,106.985L 154.746,106.985L 154.746,149.015L 154.746,156.657L 128,156.657L 128,149.015L 128,129.911L 101.254,129.911L 101.254,122.269 Z M 139.463,103.164L 139.463,95.5224L 139.463,80.2388L 192.955,80.2388L 192.955,122.269L 192.955,129.911L 166.209,129.911L 166.209,122.269L 166.209,95.5224L 166.209,103.164L 139.463,103.164 Z M 63.0447,183.403L 63.0447,175.761L 63.0447,133.731L 116.537,133.731L 116.537,175.761L 116.537,183.403L 63.0447,183.403 Z ").attr({"fill" : "0-#717171-#4A4A4A", "stroke" : 0}),
		paper.path("F1 M 45.5957,45.5956C 0.0850375,91.1062 0.0850306,164.894 45.5957,210.404C 49.2588,214.067 41.8508,207.076 45.8508,210.149C 10.7398,164.453 25.3623,109.057 67.2098,67.2097C 109.057,25.3622 164.453,10.7397 210.149,45.8507C 207.076,41.8508 214.067,49.2587 210.404,45.5956C 164.894,0.0849457 91.1063,0.0849457 45.5957,45.5956 Z ").attr({"fill" : "#FFFFFF", "opacity" : 0.2, "stroke" : 0}),
		paper.path("F1 M 210.404,210.404C 255.915,164.894 255.915,91.1065 210.404,45.5959C 206.741,41.9328 214.297,49.072 210.297,45.9986C 245.408,91.6947 230.638,146.943 188.79,188.79C 146.943,230.638 91.6946,245.408 45.9985,210.297C 49.0719,214.297 41.9327,206.741 45.5958,210.404C 91.1064,255.915 164.894,255.915 210.404,210.404 Z ").attr({fill : "#717171", "opacity" : 0.2, "stroke" : 0})
	);

	return notStartedIcon;
}
TeamBuildScreen.BuildIcon.prototype.createUnknownIcon = function (paper) {
	var unknownIcon = paper.set();

	unknownIcon.push(
		paper.ellipse(128, 128, 128, 128).attr({fill : "315-#717171-#4A4A4A", "opacity" : 1, "stroke" : 0}),
		paper.ellipse(128, 128, 116.537, 116.537).attr({fill : "135-#717171-#B8B8B8:16-#FFFFFF", "opacity" : 1, "stroke" : 0}),
		paper.path("F1 M 33.7937,108.109C 33.7937,100.924 34.8423,94.7158 36.9395,89.4833C 38.476,85.5381 40.9677,81.5721 44.4146,77.5854C 46.9271,74.6576 51.464,70.3906 58.0255,64.7843C 64.587,59.1779 68.8488,54.8019 70.8111,51.6561C 72.0557,49.6608 72.9056,47.5506 73.3607,45.3256C 73.6231,44.0426 73.7544,42.7215 73.7544,41.3623C 73.7544,34.6139 71.107,28.6909 65.8121,23.5933C 60.5172,18.4957 54.0284,15.9469 46.3456,15.9469C 38.9121,15.9469 32.7088,18.1998 27.7357,22.7056C 22.7627,27.2115 19.4976,34.2609 17.9403,43.854L 1.46196,42.023L 0,41.8606C 1.6196,28.3431 6.41613,17.987 14.3896,10.7922C 22.363,3.59738 32.9112,0 46.0342,0C 59.9254,0 71.0083,3.88808 79.2828,11.6643C 87.5574,19.4405 91.6946,28.8414 91.6946,39.8672C 91.6946,41.2482 91.6227,42.6059 91.479,43.9401C 90.9592,48.7645 89.4998,53.2832 87.1006,57.496C 84.0378,62.874 78.063,69.4977 69.1759,77.3674C 63.175,82.6622 59.261,86.5659 57.4337,89.0784C 55.6065,91.5908 54.2516,94.477 53.3691,97.737C 52.4867,100.997 51.9728,106.292 51.8274,113.622L 33.8871,113.622L 33.8771,113.026L 33.7937,108.109 Z M 33.8871,127.575L 51.8274,127.575L 51.8274,135.923L 51.8274,145.515L 33.8871,145.515L 33.8871,135.923L 33.8871,127.575 Z").attr({fill : "#4A4A4A", "stroke" : 0}).translate(82.1527,55.2423),
		paper.path("F1 M 73.7543,41.3623C 73.7543,42.6036 73.6449,43.813 73.4261,44.9907C 73.6449,46.2944 73.7543,47.6322 73.7543,49.0041C 73.7543,50.3634 73.6231,51.6845 73.3606,52.9674C 72.9055,55.1924 72.0556,57.3026 70.811,59.2979C 68.8488,62.4437 64.5869,66.8198 58.0254,72.4261C 51.464,78.0324 46.927,82.2995 44.4145,85.2272C 40.9677,89.2139 38.476,93.1799 36.9394,97.1251C 35.165,101.552 34.1413,106.678 33.8682,112.503L 33.877,113.026L 33.8871,113.622L 51.8274,113.622C 51.9727,106.292 52.4866,100.997 53.3691,97.737C 54.2516,94.4771 55.6065,91.5908 57.4337,89.0784C 59.261,86.5659 63.175,82.6622 69.1758,77.3674C 78.0629,69.4977 84.0378,62.874 87.1005,57.496C 89.4997,53.2832 90.9592,48.7646 91.479,43.9401C 91.4914,43.8254 91.5032,43.7105 91.5145,43.5954C 90.6378,34.2421 86.5606,26.1456 79.2828,19.3061C 71.0082,11.5299 59.9254,7.64182 46.0341,7.64182C 32.9112,7.64182 22.363,11.2392 14.3895,18.434C 7.96017,24.2355 3.59645,32.0925 1.29832,42.0049L 1.46194,42.0231L 17.9402,43.854C 19.4976,34.2609 22.7627,27.2115 27.7357,22.7057C 32.7088,18.1998 38.9121,15.9469 46.3456,15.9469C 54.0284,15.9469 60.5172,18.4957 65.812,23.5933C 71.1069,28.6909 73.7543,34.6139 73.7543,41.3623 Z M 51.8273,135.923L 51.8273,143.565L 51.8273,145.515L 33.8871,145.515L 33.887,143.565L 33.8871,135.923L 33.8871,135.217L 51.8273,135.217L 51.8273,135.923 Z ").attr({fill: "0-#717171-#4A4A4A", "stroke" : 0}).translate(82.1527,55.2423),
									
		paper.path("F1 M 45.5957,45.5956C 0.0850375,91.1062 0.0850306,164.894 45.5957,210.404C 49.2588,214.067 41.8508,207.076 45.8508,210.149C 10.7398,164.453 25.3623,109.057 67.2098,67.2097C 109.057,25.3622 164.453,10.7397 210.149,45.8507C 207.076,41.8508 214.067,49.2587 210.404,45.5956C 164.894,0.0849457 91.1063,0.0849457 45.5957,45.5956 Z ").attr({"fill" : "#FFFFFF", "opacity" : 0.2, "stroke" : 0}),
		paper.path("F1 M 210.404,210.404C 255.915,164.894 255.915,91.1065 210.404,45.5959C 206.741,41.9328 214.297,49.072 210.297,45.9986C 245.408,91.6947 230.638,146.943 188.79,188.79C 146.943,230.638 91.6946,245.408 45.9985,210.297C 49.0719,214.297 41.9327,206.741 45.5958,210.404C 91.1064,255.915 164.894,255.915 210.404,210.404 Z ").attr({fill : "#717171", "opacity" : 0.2, "stroke" : 0})
	);

	return unknownIcon;
}
TeamBuildScreen.BuildIcon.prototype.moveTo = function (x, y) {
	if (this.x == undefined) {
		this.x = x;
		this.y = y;
	} else {
		this.x = x - this.x;
		this.y = y - this.y;
	}

	this.icon.translate(this.x, this.y);
}
TeamBuildScreen.BuildIcon.prototype.scaleTo = function (scale) {
	this.icon.scale(scale, scale, 0, 0);

	this.scale = scale;
}
TeamBuildScreen.BuildIcon.prototype.getWidth = function () {
	return this.icon.getBBox().width;
}
TeamBuildScreen.BuildIcon.prototype.scaleToHeight = function (height) {
	this.height = height;

	var scale = height / this.nativeHeight;
	this.scaleTo(scale);
}
TeamBuildScreen.BuildIcon.prototype.createColourFromStatus = function (status) {
	if (status == "Failed" || status == "Stopped" || status == "PartiallySucceeded") {
		return "#D74C27";
	} else if (status == "Succeeded") {
		return "#4B8C41";
	}

	return "#878787";
}
TeamBuildScreen.BuildIcon.prototype.setStatus = function (paper, status) {
	if (this.status != status) {
		var colour = this.createColourFromStatus(status);
		var self = this;

		self.icon.remove();
		self.init(paper, status);
		self.scaleToHeight(self.height);
		self.icon.translate(self.x, self.y);
	}
}
TeamBuildScreen.BuildIcon.prototype.remove = function () {
	this.icon.remove();
}
TeamBuildScreen.BuildPanel = function (paper, x, y, width, height, build) {
	this.init(paper, x, y, width, height, build);
};
TeamBuildScreen.BuildPanel.prototype.init = function (paper, x, y, width, height, build) {
	this.build = build;
	this.paper = paper;

	this.createColoursFromStatus(build.status);

	this.draw(x, y, width, height);
}
TeamBuildScreen.BuildPanel.prototype.draw = function (x, y, width, height) {
	var margin = height * 0.04; // 4% of height
	var shadowOffset = 6;
	var shadowOpacity = 0.4;
	var shadowStrokeOpacity = 0.2;
	var availableHeight = height - (2 * margin);

	this.shadow = this.paper.path(
		"M " + (x + (height / 2) + shadowOffset) + "," + (y + height + shadowOffset) +
		"L " + (x + width + shadowOffset) + "," + (y + height + shadowOffset) + 
		"L " + (x + width + shadowOffset) + "," + (y + (height / 4) + shadowOffset) + 
		"C " + (x + width + shadowOffset) + "," + (y + (height / 9) + shadowOffset) + " " + (x + width + shadowOffset - (height / 9)) + "," + (y + shadowOffset) + " " + (x + width + shadowOffset - (height / 4)) + "," + (y + shadowOffset) +
		"L " + (x + (height / 2) + shadowOffset) + "," + (y + shadowOffset) + 
		"A " + (height / 2) + "," + (height / 2) + " 0 1,0 " + (x + (height / 2) + shadowOffset) + "," + (y + height + shadowOffset)
		).attr({opacity : shadowOpacity, fill : "#000000", "stroke-opacity": shadowStrokeOpacity});

	this.shadow.blur(2);

	// background
	this.background = this.paper.path(
		"M " + (x + (height / 2)) + "," + (y + height) +
		"L " + (x + width - (height / 2)) + "," + (y + height) + 
		"L " + (x + width - (height / 2)) + "," + y + 
		"L " + (x + (height / 2)) + "," + y + 
		"A " + (height / 2) + "," + (height / 2) + " 0 1,0 " + (x + (height / 2)) + "," + (y + height)
		).attr({fill : this.fromColour, "stroke-opacity" : 0});

	this.backgroundRight = this.paper.path(
		"M " + (x + width - (height / 2) - 1) + "," + (y + height) + 
		"L " + (x + width) + "," + (y + height) + 
		"L " + (x + width) + "," + (y + (height / 4)) + 
		"C " + (x + width) + "," + (y + (height / 9)) + " " + (x + width - (height / 9)) + "," + y + " " + (x + width - (height / 4)) + "," + y +
		"L " + (x + width - (height / 2) - 1) + "," + y
		).attr({fill : this.toColour, "stroke-opacity" : 0});

	this.backgroundFade = this.paper.rect(x + height + 1, y, width - height - (height / 2), height).attr({fill : this.gradient, "stroke-opacity" : 0});

	// primary icon
	this.primaryIcon = new TeamBuildScreen.BuildIcon(this.paper, this.build.status);
	this.primaryIcon.scaleToHeight(availableHeight);
	this.primaryIcon.moveTo(x + margin, y + margin);

	// overlay
	this.primaryIconOverlay = this.paper.path(
		"M " + (x + (height / 2)) + "," + (y + height) +
		"L " + (x + height) + "," + (y + height) + 
		"L " + (x + height) + "," + y + 
		"L " + (x + (height / 2)) + "," + y + 
		"A " + (height / 2) + "," + (height / 2) + " 0 1,0 " + (x + (height / 2)) + "," + (y + height))
		.attr({fill : this.fromColour, "stroke-opacity" : 0, opacity : 0})
		.hide();

	// labels
	var availableWidth = width - (3 * margin) - this.primaryIcon.getWidth();
	var labelCount = 8; // title counts for 3
	var labelHeight = availableHeight / labelCount;
	var labelX = x + this.primaryIcon.getWidth() + (2 * margin);
	var labelY = y + margin + labelHeight;

	this.labels = new Array(6);

	// title
	this.labels[0] = new TeamBuildScreen.BuildLabel(this.paper, labelX, labelY, this.build.description, 2 * labelHeight, availableWidth);

	labelY += 2 * labelHeight;
	this.labels[1] = new TeamBuildScreen.BuildLabel(this.paper, labelX, labelY, TeamBuildScreen.ConvertStatusToString(this.build.status), labelHeight, availableWidth);

	var requestedBy = this.build.requestedBy != null ? "Requested by " + this.build.requestedBy : "";
	labelY += labelHeight;
	this.labels[2] = new TeamBuildScreen.BuildLabel(this.paper, labelX, labelY, requestedBy, labelHeight, availableWidth);

	var startedOn = this.build.startedOn != null ? "Started on " + this.build.startedOn : "";
	labelY += labelHeight;
	this.labels[3] = new TeamBuildScreen.BuildLabel(this.paper, labelX, labelY, startedOn, labelHeight, availableWidth);

	var completedOn = this.build.isFinished && this.build.completedOn != null ? "Completed on " + this.build.completedOn : "";
	labelY += labelHeight;
	this.labels[4] = new TeamBuildScreen.BuildLabel(this.paper, labelX, labelY, completedOn, labelHeight, availableWidth);

	var testResults = this.build.isFinished && this.build.testResults != null ? this.build.testResults : "";
	labelY += labelHeight;
	this.labels[5] = new TeamBuildScreen.BuildLabel(this.paper, labelX, labelY, testResults, labelHeight, availableWidth);

	// progress bar
	this.progressBar = this.paper.rect(
		labelX,
		y + margin + (labelHeight * 6),
		1,
		labelHeight * 2).attr({fill : "#125400", "stroke" : 0});

	if (this.build.isFinished) {
		this.progressBar.hide();
	} else {
		this.progressBar.animate({"width" : availableWidth * this.build.progress}, 1000);
	}

	// secondary icon
    this.secondaryIcon = new TeamBuildScreen.BuildIcon(this.paper, "NotStarted");
	this.secondaryIcon.scaleToHeight(availableHeight / 2);
	this.secondaryIcon.moveTo(x + width - this.secondaryIcon.getWidth() - margin, y + margin);

	this.secondaryIconOverlay = this.paper.circle(x + width - (this.secondaryIcon.getWidth() / 2) - margin - 1, y + margin + (availableHeight / 4) - 1, (availableHeight / 4) + 2)
		.attr({fill : this.toColour, "stroke-opacity" : 0, opacity : 1}).hide();

	if (this.build.isQueued) {
		this.secondaryIconOverlay.attr({opacity : 0});
	} else {
		this.secondaryIcon.icon.hide();
	}

	if (this.build.status == "InProgress") {
		this.blink();
	}
}
TeamBuildScreen.BuildPanel.prototype.redraw = function (x, y, width, height) {
	this.stop();
	this.draw(x, y, width, height);
}
TeamBuildScreen.BuildPanel.prototype.update = function (build, paper) {
    var self = this;

    if (self.build.status != build.status) {
        var sourceFrom = self.fromColour;
        var sourceTo = self.toColour;

        self.createColoursFromStatus(build.status);
        self.newStatus = build.status;

        self.stop();
        self.primaryIconOverlay.show();
        self.primaryIconOverlay.animate({ opacity: 1 }, 500, function () {
            self.primaryIcon.setStatus(paper, self.newStatus);

            self.primaryIconOverlay.insertAfter(self.primaryIcon.icon);

            self.primaryIconOverlay.animate({ opacity: 0 }, 500, function () {
                if (self.newStatus == "InProgress") {
                    self.blink();
                }
                self.primaryIconOverlay.hide();
            });
        });

        var animHelper = paper.rect().attr({ opacity: 0, fill: sourceFrom, stroke: sourceTo });

        animHelper.onAnimation(function () {
            var from = animHelper.attr("fill");
            var to = animHelper.attr("stroke");
            var grad = "180-" + to + "-" + from;

            self.backgroundFade.attr({ fill: grad });
        });

        self.background.animate({ 'fill': self.fromColour }, 1000, function () {
            animHelper.remove()
        });
        self.primaryIconOverlay.animateWith(self.background, { 'fill': self.fromColour }, 1000);
        self.secondaryIconOverlay.animateWith(self.background, { 'fill': self.toColour }, 1000);
        self.backgroundRight.animateWith(self.background, { 'fill': self.toColour }, 1000);

        animHelper.animateWith(self.background, { fill: self.fromColour, stroke: self.toColour }, 1000, function () {
            self.backgroundFade.attr({ fill: self.gradient });
        });
    }

    if (self.build.isQueued != build.isQueued) {
        if (build.isQueued) {
            // fade queued icon in
            self.secondaryIconOverlay.show();
            self.secondaryIcon.icon.show();
            self.secondaryIconOverlay.animate({ opacity: 0 }, 1000, function () {
                self.secondaryIconOverlay.hide();
            });
        } else {
            // fade queued icon out
            self.secondaryIconOverlay.show();
            self.secondaryIconOverlay.animate({ opacity: 1 }, 1000, function () {
                self.secondaryIcon.icon.hide();
                self.secondaryIconOverlay.hide();
            });
        }
    }

    if (build.isFinished) {
        self.progressBar.hide();
        self.progressBar.attr({ "width": 1 });
    } else {
        self.progressBar.show();
        self.progressBar.animate({ "width": self.labels[1].width * build.progress }, 1000);
    }

    self.labels[1].setText(TeamBuildScreen.ConvertStatusToString(build.status));
    self.labels[2].setText(build.requestedBy != null ? "Requested by " + build.requestedBy : "");
    self.labels[3].setText(build.startedOn != null ? "Started on " + build.startedOn : "");
    var completedOn = build.isFinished && build.completedOn != null ? "Completed on " + build.completedOn : "";
    self.labels[4].setText(completedOn);
    var testResults = build.isFinished && build.testResults != null ? build.testResults : "";
    self.labels[5].setText(testResults);

    self.build = build;
}
TeamBuildScreen.BuildPanel.prototype.createColoursFromStatus = function (status) {
	if (status == "Failed" || status == "Stopped" || status == "PartiallySucceeded") {
		this.fromColour = "#D74C27";
		this.toColour = "#EDB09F";
	} else if (status == "Succeeded") {
		this.fromColour = "#4B8C41";
		this.toColour = "#AFCCAB";
	} else {
		this.fromColour = "#878787";
		this.toColour = "#CACACA";
	}

	this.gradient = "180-" + this.toColour +"-" + this.fromColour;
}
TeamBuildScreen.BuildPanel.prototype.blink = function () {
	var self = this;

	function fadeIn() {
		self.primaryIconOverlay.animate({"opacity": 1}, 1000, fadeOut);
		self.progressBar.animateWith(self.primaryIconOverlay, {"opacity": 0.25}, 1000);
	}

	function fadeOut() {
		self.primaryIconOverlay.animate({"opacity": 0}, 1000, fadeIn);
		self.progressBar.animateWith(self.primaryIconOverlay, {"opacity": 1}, 1000);
	}

	self.primaryIconOverlay.show();
	fadeIn();
}
TeamBuildScreen.BuildPanel.prototype.stop = function () {
	this.primaryIconOverlay.stop();
	this.primaryIconOverlay.animate({"opacity": 0}, 500);
	this.progressBar.stop();
	this.background.stop();
	this.secondaryIconOverlay.stop();
	this.backgroundRight.stop();
}
TeamBuildScreen.BuildGrid = function (id, buildServer, columns) {
    this.init(id, buildServer, columns);
};
TeamBuildScreen.BuildGrid.prototype.init = function (id, buildServer, columns) {
	this.margin = 16;
	this.columns = columns;
	this.buildServer = buildServer;
	this.id = id;
	this.draw();

	var currentColumn = 0;
	var currentRow = 0;
	var x = this.margin;
	var y = this.margin;

	this.panels = [];

	for (var i in this.buildServer.builds)
	{
		var build = this.buildServer.builds[i];
		var panel = new TeamBuildScreen.BuildPanel(this.paper, x, y, this.buildPanelWidth, this.buildPanelHeight, build);

		if (currentColumn < this.columns - 1) {
			currentColumn++;
			x += this.buildPanelWidth + this.margin;
		} else {
			currentColumn = 0;
			currentRow++;
			x = this.margin;
			y += this.buildPanelHeight + this.margin;
		}

		this.panels.push(panel);
	}

	var self = this;

	buildServer.register(this);
}
TeamBuildScreen.BuildGrid.prototype.draw = function () {
	this.calculateLayout();

	if (this.paper == undefined) {
		this.paper = Raphael(this.id, this.width, this.height);
	} else {
		this.paper.clear();
		this.paper.setSize(this.width, this.height);
	}

	this.paper.ellipse(this.width / 2, this.height / 2, this.width / 2, this.height / 2).attr({fill: "r#999999-#333333", stroke: 0});
}
TeamBuildScreen.BuildGrid.prototype.calculateLayout = function () {
	var count = this.buildServer.builds.length;
	var $window = $(window);
	this.width = $window.width();
	this.height = $window.height();
	this.availableWidth = this.width - (2 * this.margin);
	this.availableHeight = this.height - (2 * this.margin);
	this.buildPanelWidth = (this.availableWidth - ((this.columns - 1) * this.margin)) / this.columns;
	var rows = Math.floor(count / this.columns);
	this.buildPanelHeight = (this.availableHeight - ((rows - 1) * this.margin)) / rows;
}
TeamBuildScreen.BuildGrid.prototype.update = function (buildServer) {
	for (i = 0; i < this.panels.length; i++)
	{
		var panel = this.panels[i];
		var build = buildServer.getBuild(panel.build.description);

		panel.update(build, this.paper);
	}
}
TeamBuildScreen.BuildGrid.prototype.resize = function () {
	this.draw();

	var currentColumn = 0;
	var currentRow = 0;
	var x = this.margin;
	var y = this.margin;

	for (var i in this.panels)
	{
		var panel = this.panels[i];
		panel.redraw(x, y, this.buildPanelWidth, this.buildPanelHeight);

		if (currentColumn < this.columns - 1) {
			currentColumn++;
			x += this.buildPanelWidth + this.margin;
		} else {
			currentColumn = 0;
			currentRow++;
			x = this.margin;
			y += this.buildPanelHeight + this.margin;
		}
	}
}