var screenWidth = document.documentElement.clientWidth;
var screenHeight = document.documentElement.clientHeight;
var screenRatio = screenHeight / screenWidth;

buSetAbsoluteAll();
buSetCanvasPosition();

function buSetAbsoluteAll() {
	var all = document.getElementsByClassName("abs");
	for (var i = 0; i < all.length; i++) {
		var item = all[i];
		buSetStylePx(item.style, 0, 0, screenWidth, screenHeight);
	}
}

function buSetStylePx(style, x, y, w, h) {
	style.border = "none";
	style.position = "absolute";
	style.padding = "0px";
	style.left = x + "px";
	style.top = y + "px";
	style.width = w + "px";
	style.height = h + "px";
}


function buSetCanvasPosition() {
	var canvas = document.getElementById("unity-canvas");
	var parentNode = document.getElementById("canvas-offset");
	var style = parentNode.style;
	var canWidth = canvas.width;
	var canHeight = canvas.height;
	var canRatio = canHeight / canWidth;

	var newX = 0;
	var newY = 0;
	var newW = 0;
	var newH = 0;

	if (screenRatio < canRatio) {
		//canvasのほうが縦長//
		newW = screenWidth;
		newH = screenWidth * canRatio;
		// newY = - (newH - screenHeight) / 2;
		newY = 0;
	} else {
		newH = screenHeight;
		newW = screenHeight / canRatio;
		newX = -(newW - screenWidth) / 2;
	}
	buSetStylePx(style, newX, newY, newW, newH);
}