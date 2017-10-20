var nativeMessaging = {

	port: null,
	hostName: "com.anewtonlevey.gamesense",

	NativeMessageType: {
		RegisterEvent: 1,
        UnregisterEvent: 2,
        SendEvent: 3
	},

	connect: function(){
		var me = this;
		console.log('Connecting: ', me.hostName);

		me.port = chrome.runtime.connectNative(me.hostName);
		me.port.onMessage.addListener(me.receiveNativeMessage);
		me.port.onDisconnect.addListener(me.onDisconnected);
	},

	onDisconnected: function() {
		var me = this;
		me.port = null;
		console.warn('Disconnected: ', chrome.runtime.lastError);

	},

	sendNativeMessage: function(json) {
		var me = this;
		if(me.port === null) throw 'No connection to: ', me.hostName, ' found.';
		me.port.postMessage(json);
		console.log('Sent Message:', json);
	},

	receiveNativeMessage: function(json) {
		var me = this;
		console.log("Received:", json);
	},

	registerEvents: function() {
		var me = this;
		me.sendNativeMessage({
			type: me.NativeMessageType.RegisterEvent,
			event: "pulse"
		});

		me.sendNativeMessage({
			type: me.NativeMessageType.RegisterEvent,
			event: "flash"
		});

		me.sendNativeMessage({
			type: me.NativeMessageType.RegisterEvent,
			event: "range",
			min_value: 0,
			max_value: 100
		});
	},

	unregisterEvents: function() {
		var me = this;
		me.sendNativeMessage({
			type: me.NativeMessageType.UnregisterEvent,
			event: "pulse"
		});

		me.sendNativeMessage({
			type: me.NativeMessageType.UnregisterEvent,
			event: "flash"
		});

		me.sendNativeMessage({
			type: me.NativeMessageType.UnregisterEvent,
			event: "range"
		});
	},

	sendPulseEvent: function() {
		var me = this;
		me.sendNativeMessage({
			type: me.NativeMessageType.SendEvent,
			event: "pulse",
			data: {
				value: 1
			}
		});
	},

	sendFlashEvent: function() {
		var me = this;
		me.sendNativeMessage({
			type: me.NativeMessageType.SendEvent,
			event: "flash",
			data: {
				value: 1
			}
		});
	},

	sendRangeEvent: function(value) {
		var me = this;
		me.sendNativeMessage({
			type: me.NativeMessageType.SendEvent,
			event: "range",
			data: {
				value: Math.round(Math.random()*100) + 1
			}
		});
	}

};




console.log('Background Page Starting');
nativeMessaging.connect();



console.log("Register Events with nativeMessaging.registerEvents()");

console.log("Unregister Events with nativeMessaging.unregisterEvents()");

console.log("Send Events with nativeMessaging.send(X)Event(Y)");

