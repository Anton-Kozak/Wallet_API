function _classCallCheck(n,e){if(!(n instanceof e))throw new TypeError("Cannot call a class as a function")}function _defineProperties(n,e){for(var t=0;t<e.length;t++){var a=e[t];a.enumerable=a.enumerable||!1,a.configurable=!0,"value"in a&&(a.writable=!0),Object.defineProperty(n,a.key,a)}}function _createClass(n,e,t){return e&&_defineProperties(n.prototype,e),t&&_defineProperties(n,t),n}(window.webpackJsonp=window.webpackJsonp||[]).push([[14],{"7p6g":function(n,e,t){"use strict";t.r(e);var a=t("ofXK"),r=t("3Pt+"),i=t("fXoL"),s=t("7Vn+"),o=t("zC1p"),c=t("tyNb"),g=t("sYmb"),d=t("jhN1");function b(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.UserReq")," "))}function l(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.UserMin")," "))}function u(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.UserMax")," "))}function p(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.UserPat")," "))}function m(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.PassReq")," "))}function h(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.PassMin")," "))}function f(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.PassMax")," "))}function U(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.PassVal")," "))}function C(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.UserReq")," "))}function v(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.UserPat")," "))}function F(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.UserMin")," "))}function T(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.UserMax")," "))}function I(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.PassReq")," "))}function y(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.PassMin")," "))}function x(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.PassMax")," "))}function P(n,e){1&n&&(i.Ub(0,"div",50),i.Dc(1),i.hc(2,"translate"),i.Tb()),2&n&&(i.Cb(1),i.Fc(" ",i.ic(2,1,"Validation.PassVal")," "))}var w,k=function(n){return{flip:n}},M=function(n,e){return{"bg-danger text-light":n,"bg-success text-white":e}},_=function(n,e){return{"is-invalid":n,"is-valid":e}},D=function(n,e){return{"btn-success":n,"btn-loading":e}},O=function(n){return{"button-text":n}},S=((w=function(){function n(e,t,a,r,i){_classCallCheck(this,n),this.authService=e,this.alertify=t,this.router=a,this.translateService=r,this.titleService=i,this.isSignUp=!0,this.isSignedIn=!1,this.signInLoading=!1,this.signUpLoading=!1}return _createClass(n,[{key:"ngOnInit",value:function(){var n=this;console.log("reg start"),this.signUpForm=new r.e({usernameUp:new r.b("",[r.s.required,r.s.minLength(4),r.s.maxLength(10),r.s.pattern("[a-zA-Z0-9]+")]),userpassUp:new r.b("",[r.s.required,r.s.minLength(4),r.s.maxLength(16),r.s.pattern("([0-9].*[a-zA-Z])|([a-zA-Z].*[0-9])")])}),this.signInForm=new r.e({usernameIn:new r.b("",[r.s.required,r.s.minLength(4),r.s.maxLength(10),r.s.pattern("[a-zA-Z0-9]+")]),userpassIn:new r.b("",[r.s.required,r.s.minLength(4),r.s.maxLength(16),r.s.pattern("([0-9].*[a-zA-Z])|([a-zA-Z].*[0-9])")])}),this.setTitle(this.translateService.currentLang),this.translateService.onLangChange.subscribe((function(e){n.setTitle(e.lang)}))}},{key:"setTitle",value:function(n){"en"===n?this.titleService.setTitle("Start Now"):"ru"===n&&this.titleService.setTitle("\u0412\u0432\u043e\u0439\u0434\u0438\u0442\u0435 \u0438\u043b\u0438 \u0437\u0430\u0440\u0435\u0433\u0435\u0441\u0442\u0440\u0438\u0440\u0443\u0439\u0442\u0435\u0441\u044c")}},{key:"onSignUp",value:function(){var n=this;this.signUpLoading=!0,this.authService.register(this.signUpForm.value.usernameUp,this.signUpForm.value.userpassUp,"Adult").subscribe((function(e){n.alertify.success(e.data),n.signUpForm.reset(),n.signInForm.reset(),n.isSignUp=!1,n.signUpLoading=!1}),(function(e){n.alertify.error(e.error),n.signUpLoading=!1}))}},{key:"onSignIn",value:function(){var n=this;this.signInLoading=!0,this.authService.login(this.signInForm.value.usernameIn,this.signInForm.value.userpassIn).subscribe((function(e){n.alertify.success("Welcome: "+e.user.userName),n.hasWallet()?(n.authService.hasWallet.next(!0),n.router.navigate(["/wallet/home-wallet"])):(console.log("I have no wallet"),n.router.navigate(["/main/no-wallet"])),n.signInLoading=!1}),(function(e){n.alertify.error("Incorrect username or password"),console.log(e),n.signInLoading=!1}))}},{key:"switchCard",value:function(){this.isSignUp=!this.isSignUp,this.signUpForm.reset(),this.signInForm.reset()}},{key:"hasWallet",value:function(){return null!==this.authService.getToken()&&(console.log(this.authService.getToken()),"true"===this.authService.getToken().hasWallet)}},{key:"logout",value:function(){this.authService.logout()}}]),n}()).\u0275fac=function(n){return new(n||w)(i.Ob(s.a),i.Ob(o.a),i.Ob(c.b),i.Ob(g.d),i.Ob(d.d))},w.\u0275cmp=i.Ib({type:w,selectors:[["app-signup-signin"]],decls:155,vars:151,consts:[[1,"outer"],["id","reg-section"],[1,"dark-overlay","align-content-center"],[1,"home-inner","pt-5","container"],[1,"row","mt-5"],[1,"col-lg-8","d-none","d-lg-block"],["data-aos","fade-down","data-aos-duration","1200","data-aos-once","true","data-aos-delay","0",1,"display-4","text-capitalize","text-light","title"],["data-aos","fade-down","data-aos-duration","1200","data-aos-once","true","data-aos-delay","100",1,"d-flex"],[1,"p-4","align-self-start"],[1,"fas","fa-check","fa-3x","text-light"],[1,"p-4","align-self-end","text-light","benefits"],["data-aos","fade-left","data-aos-duration","1200","data-aos-once","true","data-aos-delay","200",1,"d-flex"],["data-aos","fade-left","data-aos-duration","1200","data-aos-once","true","data-aos-delay","300",1,"d-flex"],["data-aos","fade-down","data-aos-duration","1200","data-aos-once","true","data-aos-delay","0",1,"col-lg-4"],[1,"card-container"],[1,"card-flip",3,"ngClass"],[1,"card","front","regform","text-center","card-form"],[1,"card-body","text-light","shadow-lg"],[1,"border-primary"],[3,"formGroup","ngSubmit"],[1,"form-group"],[1,"input-group","mb-3"],[1,"input-group-prepend","shadow-lg"],[1,"input-group-text",3,"ngClass"],["type","text","formControlName","usernameUp",1,"form-control",3,"placeholder","ngClass"],["class","invalid-feedback",4,"ngIf"],["type","password","formControlName","userpassUp",1,"form-control",3,"placeholder","ngClass"],[1,"btn","btn-secondary","btn-block",3,"disabled","ngClass"],[3,"ngClass"],[1,"container"],[1,"text-light",2,"cursor","pointer",3,"click"],[1,"card","back","regform","text-center","card-form"],["type","text","formControlName","usernameIn",1,"form-control",3,"placeholder","ngClass"],[1,"input-group","mb-5"],["type","password","formControlName","userpassIn",1,"form-control",3,"placeholder","ngClass"],[1,"form-group","mb-3"],[1,"bg-light","text-muted","py-5"],[1,"row","align-items-center"],["data-aos","fade-right","data-aos-duration","1200","data-aos-once","true","data-aos-delay","0",1,"col-md-6"],["src","assets\\images\\reg\\img.jpg","alt","",1,"img-fluid"],[1,"col-md-6"],["data-aos","fade-down","data-aos-duration","1200","data-aos-once","true","data-aos-delay","100",1,"why"],["data-aos","fade-right","data-aos-duration","1200","data-aos-once","true","data-aos-delay","200",1,"d-flex"],[1,"p-4","benefits"],["data-aos","fade-right","data-aos-duration","1200","data-aos-once","true","data-aos-delay","400",1,"d-flex"],["data-aos","fade-right","data-aos-duration","1200","data-aos-once","true","data-aos-delay","600",1,"d-flex","align-items-center"],["id","main-footer",1,"bg-dark"],[1,"row"],[1,"col","text-center","py-4"],["id","year"],[1,"invalid-feedback"]],template:function(n,e){1&n&&(i.Ub(0,"div",0),i.Ub(1,"header",1),i.Ub(2,"div",2),i.Ub(3,"div",3),i.Ub(4,"div",4),i.Ub(5,"div",5),i.Ub(6,"p",6),i.Dc(7),i.hc(8,"translate"),i.Tb(),i.Ub(9,"div",7),i.Ub(10,"div",8),i.Pb(11,"i",9),i.Tb(),i.Ub(12,"div",10),i.Dc(13),i.hc(14,"translate"),i.Tb(),i.Tb(),i.Ub(15,"div",11),i.Ub(16,"div",8),i.Pb(17,"i",9),i.Tb(),i.Ub(18,"div",10),i.Dc(19),i.hc(20,"translate"),i.Tb(),i.Tb(),i.Ub(21,"div",12),i.Ub(22,"div",8),i.Pb(23,"i",9),i.Tb(),i.Ub(24,"div",10),i.Dc(25),i.hc(26,"translate"),i.Tb(),i.Tb(),i.Tb(),i.Ub(27,"div",13),i.Ub(28,"div",14),i.Ub(29,"div",15),i.Ub(30,"div",16),i.Ub(31,"div",17),i.Ub(32,"h1"),i.Dc(33),i.hc(34,"translate"),i.Tb(),i.Ub(35,"p"),i.Dc(36),i.hc(37,"translate"),i.Tb(),i.Ub(38,"div",18),i.Ub(39,"form",19),i.cc("ngSubmit",(function(){return e.onSignUp()})),i.Ub(40,"div",20),i.Ub(41,"div",21),i.Ub(42,"div",22),i.Ub(43,"span",23),i.Dc(44),i.hc(45,"translate"),i.Tb(),i.Tb(),i.Pb(46,"input",24),i.hc(47,"translate"),i.Cc(48,b,3,3,"div",25),i.Cc(49,l,3,3,"div",25),i.Cc(50,u,3,3,"div",25),i.Cc(51,p,3,3,"div",25),i.Tb(),i.Tb(),i.Ub(52,"div",20),i.Ub(53,"div",21),i.Ub(54,"div",22),i.Ub(55,"span",23),i.Dc(56),i.hc(57,"translate"),i.Tb(),i.Tb(),i.Pb(58,"input",26),i.hc(59,"translate"),i.Cc(60,m,3,3,"div",25),i.Cc(61,h,3,3,"div",25),i.Cc(62,f,3,3,"div",25),i.Cc(63,U,3,3,"div",25),i.Tb(),i.Tb(),i.Ub(64,"div",20),i.Ub(65,"button",27),i.Ub(66,"span",28),i.Dc(67),i.hc(68,"translate"),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Ub(69,"div",29),i.Ub(70,"p"),i.Dc(71),i.hc(72,"translate"),i.Ub(73,"a",30),i.cc("click",(function(){return e.switchCard()})),i.Ub(74,"strong"),i.Dc(75),i.hc(76,"translate"),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Ub(77,"div",31),i.Ub(78,"div",17),i.Ub(79,"h1"),i.Dc(80),i.hc(81,"translate"),i.Tb(),i.Ub(82,"p"),i.Dc(83),i.hc(84,"translate"),i.Tb(),i.Ub(85,"div",18),i.Ub(86,"form",19),i.cc("ngSubmit",(function(){return e.onSignIn()})),i.Ub(87,"div",20),i.Ub(88,"div",21),i.Ub(89,"div",22),i.Ub(90,"span",23),i.Dc(91),i.hc(92,"translate"),i.Tb(),i.Tb(),i.Pb(93,"input",32),i.hc(94,"translate"),i.Cc(95,C,3,3,"div",25),i.Cc(96,v,3,3,"div",25),i.Cc(97,F,3,3,"div",25),i.Cc(98,T,3,3,"div",25),i.Tb(),i.Tb(),i.Ub(99,"div",20),i.Ub(100,"div",33),i.Ub(101,"div",22),i.Ub(102,"span",23),i.Dc(103),i.hc(104,"translate"),i.Tb(),i.Tb(),i.Pb(105,"input",34),i.hc(106,"translate"),i.Cc(107,I,3,3,"div",25),i.Cc(108,y,3,3,"div",25),i.Cc(109,x,3,3,"div",25),i.Cc(110,P,3,3,"div",25),i.Tb(),i.Tb(),i.Ub(111,"div",35),i.Ub(112,"button",27),i.Ub(113,"span",28),i.Dc(114),i.hc(115,"translate"),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Ub(116,"div",29),i.Ub(117,"p"),i.Dc(118),i.hc(119,"translate"),i.Ub(120,"a",30),i.cc("click",(function(){return e.isSignUp=!e.isSignUp})),i.Ub(121,"strong"),i.Dc(122),i.hc(123,"translate"),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Ub(124,"section",36),i.Ub(125,"div",29),i.Ub(126,"div",37),i.Ub(127,"div",38),i.Pb(128,"img",39),i.Tb(),i.Ub(129,"div",40),i.Ub(130,"p",41),i.Dc(131),i.hc(132,"translate"),i.Tb(),i.Ub(133,"div",42),i.Ub(134,"div",43),i.Dc(135),i.hc(136,"translate"),i.Tb(),i.Tb(),i.Ub(137,"div",44),i.Ub(138,"div",43),i.Dc(139),i.hc(140,"translate"),i.Tb(),i.Tb(),i.Ub(141,"div",45),i.Ub(142,"div",43),i.Dc(143),i.hc(144,"translate"),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Ub(145,"footer",46),i.Ub(146,"div",29),i.Ub(147,"div",47),i.Ub(148,"div",48),i.Ub(149,"h3"),i.Dc(150,"XPENSE"),i.Tb(),i.Ub(151,"p"),i.Dc(152,"Copyright \xa9 "),i.Ub(153,"span",49),i.Dc(154,"2020"),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb(),i.Tb()),2&n&&(i.Cb(7),i.Fc(" ",i.ic(8,63,"Registration.Benefits"),""),i.Cb(6),i.Fc(" ",i.ic(14,65,"Registration.Benefit1")," "),i.Cb(6),i.Fc(" ",i.ic(20,67,"Registration.Benefit2")," "),i.Cb(6),i.Fc(" ",i.ic(26,69,"Registration.Benefits")," "),i.Cb(4),i.mc("ngClass",i.pc(115,k,!e.isSignUp)),i.Cb(4),i.Fc(" ",i.ic(34,71,"Registration.SignUp"),""),i.Cb(3),i.Fc(" ",i.ic(37,73,"Registration.Fill"),""),i.Cb(3),i.mc("formGroup",e.signUpForm),i.Cb(4),i.mc("ngClass",i.qc(117,M,e.signUpForm.get("usernameUp").errors&&e.signUpForm.get("usernameUp").touched,!e.signUpForm.get("usernameUp").errors)),i.Cb(1),i.Fc(" ",i.ic(45,75,"Registration.Username"),""),i.Cb(2),i.nc("placeholder",i.ic(47,77,"Placeholders.Username")),i.mc("ngClass",i.qc(120,_,e.signUpForm.get("usernameUp").errors&&e.signUpForm.get("usernameUp").touched,!e.signUpForm.get("usernameUp").errors)),i.Cb(2),i.mc("ngIf",e.signUpForm.get("usernameUp").hasError("required")&&e.signUpForm.get("usernameUp").touched),i.Cb(1),i.mc("ngIf",e.signUpForm.get("usernameUp").hasError("minlength")&&e.signUpForm.get("usernameUp").touched),i.Cb(1),i.mc("ngIf",e.signUpForm.get("usernameUp").hasError("maxlength")&&e.signUpForm.get("usernameUp").touched),i.Cb(1),i.mc("ngIf",e.signUpForm.get("usernameUp").hasError("pattern")&&e.signUpForm.get("usernameUp").touched),i.Cb(4),i.mc("ngClass",i.qc(123,M,e.signUpForm.get("userpassUp").errors&&e.signUpForm.get("userpassUp").touched,!e.signUpForm.get("userpassUp").errors)),i.Cb(1),i.Fc(" ",i.ic(57,79,"Registration.Password"),""),i.Cb(2),i.nc("placeholder",i.ic(59,81,"Placeholders.Password")),i.mc("ngClass",i.qc(126,_,e.signUpForm.get("userpassUp").errors&&e.signUpForm.get("userpassUp").touched,!e.signUpForm.get("userpassUp").errors)),i.Cb(2),i.mc("ngIf",e.signUpForm.get("userpassUp").hasError("required")&&e.signUpForm.get("userpassUp").touched),i.Cb(1),i.mc("ngIf",e.signUpForm.get("userpassUp").hasError("minlength")&&e.signUpForm.get("userpassUp").touched),i.Cb(1),i.mc("ngIf",e.signUpForm.get("userpassUp").hasError("maxlength")&&e.signUpForm.get("userpassUp").touched),i.Cb(1),i.mc("ngIf",e.signUpForm.get("userpassUp").hasError("pattern")&&e.signUpForm.get("userpassUp").touched),i.Cb(2),i.Fb("spinner",e.signUpLoading),i.mc("disabled",e.signUpForm.invalid&&!e.signUpLoading)("ngClass",i.qc(129,D,e.signUpForm.valid,e.signUpLoading)),i.Cb(1),i.mc("ngClass",i.pc(132,O,e.signUpLoading)),i.Cb(1),i.Ec(i.ic(68,83,"Registration.Create")),i.Cb(4),i.Fc(" ",i.ic(72,85,"Registration.SignInTip")," "),i.Cb(4),i.Fc(" ",i.ic(76,87,"Registration.SignInHere"),""),i.Cb(5),i.Fc(" ",i.ic(81,89,"Registration.SignIn"),""),i.Cb(3),i.Fc(" ",i.ic(84,91,"Registration.Please"),""),i.Cb(3),i.mc("formGroup",e.signInForm),i.Cb(4),i.mc("ngClass",i.qc(134,M,e.signInForm.get("usernameIn").errors&&e.signInForm.get("usernameIn").touched,!e.signInForm.get("usernameIn").errors)),i.Cb(1),i.Fc(" ",i.ic(92,93,"Registration.Username"),""),i.Cb(2),i.nc("placeholder",i.ic(94,95,"Placeholders.Username")),i.mc("ngClass",i.qc(137,_,e.signInForm.get("usernameIn").errors&&e.signInForm.get("usernameIn").touched,!e.signInForm.get("usernameIn").errors)),i.Cb(2),i.mc("ngIf",e.signInForm.get("usernameIn").hasError("required")&&e.signInForm.get("usernameIn").touched),i.Cb(1),i.mc("ngIf",e.signInForm.get("usernameIn").hasError("pattern")&&e.signInForm.get("usernameIn").touched),i.Cb(1),i.mc("ngIf",e.signInForm.get("usernameIn").hasError("minlength")&&e.signInForm.get("usernameIn").touched),i.Cb(1),i.mc("ngIf",e.signInForm.get("usernameIn").hasError("maxlength")&&e.signInForm.get("usernameIn").touched),i.Cb(4),i.mc("ngClass",i.qc(140,M,e.signInForm.get("userpassIn").errors&&e.signInForm.get("userpassIn").touched,!e.signInForm.get("userpassIn").errors)),i.Cb(1),i.Fc(" ",i.ic(104,97,"Registration.Password"),""),i.Cb(2),i.nc("placeholder",i.ic(106,99,"Placeholders.Password")),i.mc("ngClass",i.qc(143,_,e.signInForm.get("userpassIn").errors&&e.signInForm.get("userpassIn").touched,!e.signInForm.get("userpassIn").errors)),i.Cb(2),i.mc("ngIf",e.signInForm.get("userpassIn").hasError("required")&&e.signInForm.get("userpassIn").touched),i.Cb(1),i.mc("ngIf",e.signInForm.get("userpassIn").hasError("minlength")&&e.signInForm.get("userpassIn").touched),i.Cb(1),i.mc("ngIf",e.signInForm.get("userpassIn").hasError("maxlength")&&e.signInForm.get("userpassIn").touched),i.Cb(1),i.mc("ngIf",e.signInForm.get("userpassIn").hasError("pattern")&&e.signInForm.get("userpassIn").touched),i.Cb(2),i.Fb("spinner",e.signInLoading),i.mc("disabled",e.signInForm.invalid&&!e.signInLoading)("ngClass",i.qc(146,D,e.signInForm.valid,e.signInLoading)),i.Cb(1),i.mc("ngClass",i.pc(149,O,e.signInLoading)),i.Cb(1),i.Ec(i.ic(115,101,"Registration.Signin")),i.Cb(4),i.Fc(" ",i.ic(119,103,"Registration.SignUpTip")," "),i.Cb(4),i.Fc(" ",i.ic(123,105,"Registration.SignUpHere"),""),i.Cb(9),i.Fc(" ",i.ic(132,107,"Registration.Why"),""),i.Cb(4),i.Fc(" ",i.ic(136,109,"Registration.Expl1")," "),i.Cb(4),i.Fc(" ",i.ic(140,111,"Registration.Expl2")," "),i.Cb(4),i.Fc(" ",i.ic(144,113,"Registration.Expl3")," "))},directives:[a.j,r.u,r.l,r.f,r.a,r.k,r.d,a.l],pipes:[g.c],styles:['.invalid-feedback[_ngcontent-%COMP%]{font-weight:bolder;text-shadow:-1px 1px 7px rgba(0,0,0,.95)}.why[_ngcontent-%COMP%]{line-height:1.3}.title[_ngcontent-%COMP%], .why[_ngcontent-%COMP%]{font-size:30px;font-weight:700}.benefits[_ngcontent-%COMP%]{font-size:22px;font-weight:300;line-height:1.5;color:#000}#reg-section[_ngcontent-%COMP%]{background-image:url(main.eed842fe540281db819e.jpg);background-repeat:no-repeat;background-size:cover;background-attachment:fixed;min-height:900px;width:100%}#reg-section[_ngcontent-%COMP%]   .dark-overlay[_ngcontent-%COMP%]{position:absolute;top:0;left:0;width:100%;min-height:900px;background-color:rgba(0,0,0,.75)}.invalid-feedback[_ngcontent-%COMP%]{font-size:13px;opacity:1}.regform[_ngcontent-%COMP%]{background-color:#264653!important}.card-container[_ngcontent-%COMP%], .card-flip[_ngcontent-%COMP%]{transform-style:preserve-3d;transition:all .7s ease}.card-flip[_ngcontent-%COMP%]   div[_ngcontent-%COMP%]{-webkit-backface-visibility:hidden;backface-visibility:hidden;transform-style:preserve-3d}.card-img-top[_ngcontent-%COMP%]{height:70px;-o-object-fit:cover;object-fit:cover}.flip[_ngcontent-%COMP%]{transform:rotateY(180deg)}.card-flip[_ngcontent-%COMP%]{display:grid;grid-template:1fr/1fr;grid-template-areas:"frontAndBack";transform-style:preserve-3d;transition:all .7s ease}.back[_ngcontent-%COMP%], .front[_ngcontent-%COMP%]{grid-area:frontAndBack}.back[_ngcontent-%COMP%]{transform:rotateY(-180deg)}.card-container[_ngcontent-%COMP%]{display:grid;perspective:700px}#explore-head-section[_ngcontent-%COMP%]{background:#333}[_ngcontent-%COMP%]::-webkit-input-placeholder{font-size:12px}[_ngcontent-%COMP%]::-moz-placeholder{font-size:12px}[_ngcontent-%COMP%]::-ms-input-placeholder{font-size:12px}[_ngcontent-%COMP%]::placeholder{font-size:12px}.input-group-prepend[_ngcontent-%COMP%]{width:110px;text-align:center}.input-group-prepend[_ngcontent-%COMP%]   .input-group-text[_ngcontent-%COMP%]{width:100%;justify-content:center}@-webkit-keyframes spinner{to{transform:rotate(1turn)}}@keyframes spinner{to{transform:rotate(1turn)}}.spinner[_ngcontent-%COMP%]:before{content:"";box-sizing:border-box;position:absolute;top:50%;left:50%;width:30px;height:30px;margin-top:-15px;margin-left:-15px;border-radius:50%;border:2px solid #00ff37;border-top-color:#00a738;-webkit-animation:spinner .8s linear infinite;animation:spinner .8s linear infinite}.btn-loading[_ngcontent-%COMP%]{background-color:rgba(10,48,19,.43)}.button-text[_ngcontent-%COMP%]{opacity:.3}']}),w),L=t("CLZf");t.d(e,"StartNowModule",(function(){return E}));var R,E=((R=function n(){_classCallCheck(this,n)}).\u0275mod=i.Mb({type:R}),R.\u0275inj=i.Lb({factory:function(n){return new(n||R)},imports:[[a.c,c.f.forChild([{path:"",component:S}]),r.g,r.q,L.a]]}),R)},zC1p:function(n,e,t){"use strict";t.d(e,"a",(function(){return r}));var a=t("fXoL"),r=function(){var n=function(){function n(){_classCallCheck(this,n)}return _createClass(n,[{key:"confirm",value:function(n,e){alertify.confirm(n,(function(n){n&&e()}))}},{key:"success",value:function(n){alertify.success(n)}},{key:"error",value:function(n){alertify.error(n)}},{key:"warning",value:function(n){alertify.warning(n)}},{key:"message",value:function(n){alertify.message(n)}}]),n}();return n.\u0275fac=function(e){return new(e||n)},n.\u0275prov=a.Kb({token:n,factory:n.\u0275fac,providedIn:"root"}),n}()}}]);