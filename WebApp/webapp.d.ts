/**Jquery Promise */
interface IPromise {
	then(success: (result: any) => void | IPromise, fail: (result: any) => void | IPromise): IPromise;
	done(callback: (result: any) => void | IPromise): IPromise;
	fail(callback: (result: any) => void | IPromise): IPromise;
}
/**A dictionary with key is shortcut and value is action function */
interface IHotkeyConfig {
	[keyShorcut: string]: () => void;
}
interface IButtonDialogConfig {
	/**value of button, defined 'submit', 'close' */
	value: string;
	/**text of button */
	text: string;
	/**DevExpress icon string, 16px */
	icon: string;
	/**Optional, key shortcut */
	hotkey?: string;
	/**process when button clicked */
	action: () => void;
}
interface IDialogConfig {
	/**Optional, Unique name */
	name?: string;
	/**Show in title of dialog */
	title: string;
	/**URL to get content showing in dialog */
	url: string;
	/**Parameter used when get content showing in dialog */
	data: any;
	/**Optional, DevExpress icon string, 32px */
	icon?: string;
	/**Optional, Show dialog with maximize, default false */
	maximized?: boolean;
	/**Optional, Show dialog in model mode, defaul true */
	modal?: boolean;
	/**Optional, Hotkey used in dialog  */
	hotkey?: IHotkeyConfig;
	/**Optional, Width of dialog, default 300 */
	width?: number;
	/**Optional,  buttons in dialog, default Submit and Close buttons*/
	buttons?: IButtonDialogConfig[];
	/**Optional, Event after show dialog */
	init?: () => void;
	/**Optional, Event before submit form, return false to cancel submit process */
	beforeSubmit?: () => boolean | IPromise;
	/**Optional, Event when resize dialog */
	resize?: () => void;
	/**This object is created by WebApp framework and it use in button.action to raise callback promise of dialog */
	deferred?: { resolve: (r?: any) => void, reject: (r?: any) => void };
}

interface IBaseFunction {
	title?: string;
	hotkey?: IHotkeyConfig;
	init?: () => void;
	resize?: () => void;
}

interface IWebApp {
   title: string;
	/**default message string */
	message: {
		error: string;
		errData: string;
		errServer: string;
		saved: string;
		delete: string;
		deleteConfirm: string;
	};

   /**
    * Show/Hide loading panel
    * @param b true: show, false: hide
    */
	loading(b?: boolean): boolean;

	hotkeys: {
		active(cfg: IHotkeyConfig): void;
		deactive(cfg: IHotkeyConfig): void;
	}

   /**
    * Show a message box to display some information with close button
    * Return done callback after close
    * @param title Title caption
    * @param message Message text
    */
	showInfo(title: string, message: string): IPromise;

   /**
    * Show a message box to get confirm before do some things
    * Return done callback if click OK, else is fail callback
    * @param title Title caption
    * @param message Message text
    */
	showConfirm(title: string, message: string): IPromise;

   /**
    * Call jquery ajax to server
    * Return promise of jquery ajax
    * @param url Url request
    * @param data Parameter request
    * @param method 'get' or 'post' (default)
    */
	ajax(url: string, data: any, method?: string): IPromise;

   /**
    * Open a function in WebApp framework
    * @param url URL of function
    */
	openMenu(url: string): void;

   /**
    * Refresh web app with new language
    * @param code ISO code of langeuage (vi,en,...)
    */
	changeLanguage(code: string);

   /**
    * Refresh web app with new skin
    * @param name skin named of DevExpress
    */
	changeSkin(name: string);

	/**Show a dialog to change password of current user */
	changePassword(): void;

   /**
    * Add js unobtrusive validation for loaded form
    * @param frm a form element, string or DOM or jquery
    */
	validateForm(frm: any);

   /**
    * Validate data in a form
    * @param frm a form element, string or DOM or jquery
    */
	validForm(frm: any): boolean;

   /**
    * Submit data in a form to server by Ajax, form must has attr action="url used to post"
    * @param frm a form element, string or DOM or jquery
    */
	submit(frm: any): IPromise;

   /**
    * Show a custom dialog, return callback promise depend on button clicked, default Submit is 'done', Close is 'fail'
    * @param cfg dialog config
    */
	showDialog(cfg: IDialogConfig): IPromise;

   /**
    * Show a toast message
    * @param message message text
    * @param type message type: info|success|warning|error, default is success
    */
	notify(message: string, type?: string): void;

	/**Logoff current user */
	exit(): void;

	/**Show a message box about deny access*/
	AccessDeny(): void;

	/**Close current function */
	closeFunction(): void;

   /**
    * Show preview current report
    * @param url URL of action to set current report
    * @param data parameter request
    */
	showPreview(url: string, data: any): IPromise;

   /**
    * Show print current report
    * @param url URL of action to set current report
    * @param data parameter request
    */
	showPrint(url: string, data: any): IPromise;

   function: IBaseFunction;

   /**Dictionary to filter unicode by ascii code*/
   CustomFilterDictionary: { [key: string]: string };
   /**
    * Get RegExp with custom ascii code from dictionary to search unicode
    * @param filter filter string
    */
   getCustomFilterRegExp(filter: string): RegExp;
}

declare const WebApp: IWebApp;

interface String {
	format(...args: any[]): string;
	startsWith(s: string): boolean;
	endsWith(s: string): boolean;
}

interface Number {
	format(format: string): string;
}