(window.webpackJsonp=window.webpackJsonp||[]).push([[16],{"0xaD":function(e,t,s){"use strict";s.d(t,"a",(function(){return o}));var n=s("AytR"),i=s("2Vo4"),c=s("XNiG"),a=s("lJxs"),r=s("fXoL"),b=s("tk/3"),h=s("7Vn+");let o=(()=>{class e{constructor(e,t){this.http=e,this.authService=t,this.baseUrl=n.a.apiUrl+"expense/",this.initialExpenses=[],this.expensesSubject=new i.a(0),this.categoryTitles=new c.a,this.firstSubject=new c.a,this.secondSubject=new c.a,this.thirdSubject=new c.a,this.fourthSubject=new c.a,this.fifthSubject=new c.a,this.sixthSubject=new c.a,this.seventhSubject=new c.a,this.eightthSubject=new c.a,this.ninethSubject=new c.a,this.tenthSubject=new c.a,this.firstExpenses={categoryName:"",expenses:[],categoryId:0},this.secondExpenses={categoryName:"",expenses:[],categoryId:0},this.thirdExpenses={categoryName:"",expenses:[],categoryId:0},this.fourthExpenses={categoryName:"",expenses:[],categoryId:0},this.fifthExpenses={categoryName:"",expenses:[],categoryId:0},this.sixthExpenses={categoryName:"",expenses:[],categoryId:0},this.seventhExpenses={categoryName:"",expenses:[],categoryId:0},this.eightthExpenses={categoryName:"",expenses:[],categoryId:0},this.ninethExpenses={categoryName:"",expenses:[],categoryId:0},this.tenthExpenses={categoryName:"",expenses:[],categoryId:0}}showAllExpenses(){return this.http.get(this.baseUrl+this.authService.getToken().nameid).subscribe(e=>{if(null!=e){console.log(e);let t=0,s=[];this.firstExpenses.expenses=e[0].expenses,this.firstExpenses.categoryId=e[0].categoryId,this.firstExpenses.categoryName=e[0].categoryName,this.firstSubject.next(this.firstExpenses.expenses),t++,s.push({id:this.firstExpenses.categoryId,title:this.firstExpenses.categoryName}),this.secondExpenses.expenses=e[1].expenses,this.secondExpenses.categoryId=e[1].categoryId,this.secondExpenses.categoryName=e[1].categoryName,this.secondSubject.next(this.secondExpenses.expenses),t++,s.push({id:this.secondExpenses.categoryId,title:this.secondExpenses.categoryName}),this.thirdExpenses.expenses=e[2].expenses,this.thirdExpenses.categoryId=e[2].categoryId,this.thirdExpenses.categoryName=e[2].categoryName,this.thirdSubject.next(this.thirdExpenses.expenses),t++,s.push({id:this.thirdExpenses.categoryId,title:this.thirdExpenses.categoryName}),this.fourthExpenses.expenses=e[3].expenses,this.fourthExpenses.categoryId=e[3].categoryId,this.fourthExpenses.categoryName=e[3].categoryName,this.fourthSubject.next(this.fourthExpenses.expenses),t++,s.push({id:this.fourthExpenses.categoryId,title:this.fourthExpenses.categoryName}),this.fifthExpenses.expenses=e[4].expenses,this.fifthExpenses.categoryId=e[4].categoryId,this.fifthExpenses.categoryName=e[4].categoryName,this.fifthSubject.next(this.fifthExpenses.expenses),t++,s.push({id:this.fifthExpenses.categoryId,title:this.fifthExpenses.categoryName}),t<e.length&&(this.sixthExpenses.expenses=e[5].expenses,this.sixthExpenses.categoryId=e[5].categoryId,this.sixthExpenses.categoryName=e[5].categoryName,this.sixthSubject.next(this.sixthExpenses.expenses),t++,s.push({id:this.sixthExpenses.categoryId,title:this.sixthExpenses.categoryName})),t<e.length&&(this.seventhExpenses.expenses=e[6].expenses,this.seventhExpenses.categoryId=e[6].categoryId,this.seventhExpenses.categoryName=e[6].categoryName,this.seventhSubject.next(this.seventhExpenses.expenses),t++,s.push({id:this.seventhExpenses.categoryId,title:this.seventhExpenses.categoryName})),t<e.length&&(this.eightthExpenses.expenses=e[7].expenses,this.eightthExpenses.categoryId=e[7].categoryId,this.eightthExpenses.categoryName=e[7].categoryName,this.eightthSubject.next(this.eightthExpenses.expenses),t++,s.push({id:this.eightthExpenses.categoryId,title:this.eightthExpenses.categoryName})),t<e.length&&(this.ninethExpenses.expenses=e[8].expenses,this.ninethExpenses.categoryId=e[8].categoryId,this.ninethExpenses.categoryName=e[8].categoryName,this.ninethSubject.next(this.ninethExpenses.expenses),t++,s.push({id:this.ninethExpenses.categoryId,title:this.ninethExpenses.categoryName})),t<e.length&&(this.tenthExpenses.expenses=e[9].expenses,this.tenthExpenses.categoryId=e[9].categoryId,this.tenthExpenses.categoryName=e[9].categoryName,this.tenthSubject.next(this.tenthExpenses.expenses),t++,s.push({id:this.tenthExpenses.categoryId,title:this.tenthExpenses.categoryName})),this.categoryTitles.next(s)}})}showDailyExpenses(e){return this.http.get(this.baseUrl+this.authService.getToken().nameid+"/dailyExpenses/"+e)}getPreviousExpenses(e){return this.http.get(this.baseUrl+this.authService.getToken().nameid+"/previousExpenses/"+e)}createExpense(e){return this.http.post(this.baseUrl+this.authService.getToken().nameid+"/new",e).pipe(Object(a.a)(t=>{var s=t.expense;switch(+e.expenseCategoryId){case this.firstExpenses.categoryId:this.firstExpenses.expenses.push(s),console.log("pushed meat",this.firstExpenses.expenses),this.firstSubject.next(this.firstExpenses.expenses),console.log("nexted meat",this.firstSubject);break;case this.secondExpenses.categoryId:this.secondExpenses.expenses.push(s),this.secondSubject.next(this.secondExpenses.expenses);break;case this.thirdExpenses.categoryId:this.thirdExpenses.expenses.push(s),this.thirdSubject.next(this.thirdExpenses.expenses);break;case this.fourthExpenses.categoryId:this.fourthExpenses.expenses.push(s),this.fourthSubject.next(this.fourthExpenses.expenses);break;case this.fifthExpenses.categoryId:this.fifthExpenses.expenses.push(s),this.fifthSubject.next(this.fifthExpenses.expenses);break;case this.sixthExpenses.categoryId:this.sixthExpenses.expenses.push(s),this.sixthSubject.next(this.sixthExpenses.expenses);break;case this.seventhExpenses.categoryId:this.seventhExpenses.expenses.push(s),this.seventhSubject.next(this.seventhExpenses.expenses);break;case this.eightthExpenses.categoryId:this.eightthExpenses.expenses.push(s),this.eightthSubject.next(this.eightthExpenses.expenses);break;case this.ninethExpenses.categoryId:this.ninethExpenses.expenses.push(s),this.ninethSubject.next(this.ninethExpenses.expenses);break;case this.tenthExpenses.categoryId:this.tenthExpenses.expenses.push(s),this.tenthSubject.next(this.tenthExpenses.expenses)}return this.expensesSubject.next(this.expensesSubject.getValue()+s.moneySpent),t}))}getWalletStatistics(e){return this.http.get(this.baseUrl+this.authService.getToken().nameid+"/detailedStatistics/"+e)}getCategoryStatistics(e,t){return this.http.get(this.baseUrl+this.authService.getToken().nameid+"/detailedCategoryStatistics/"+e+"/"+t)}getUserStatistics(e,t){return this.http.get(this.baseUrl+this.authService.getToken().nameid+"/detailedUserStatistics/"+e+"/"+t)}getUserExpenses(e,t){return this.http.get(this.baseUrl+this.authService.getToken().nameid+"/getUserExpenses/"+e+"/"+t)}onExpenseDelete(e){return this.http.delete(this.baseUrl+this.authService.getToken().nameid+"/expenseDelete/"+e,{responseType:"text"})}onExpenseEdit(e){return console.log("edit expene",e),this.http.post(this.baseUrl+this.authService.getToken().nameid+"/expenseEdit/"+e.id,e)}getWalletData(e){return this.http.get(this.baseUrl+e+"/getNameAndLimit").pipe(Object(a.a)(e=>(this.expensesSubject.next(e.monthlyExpenses),e)))}getSpecifiedMonthsData(e,t){return this.http.get(this.baseUrl+this.authService.getToken().nameid+"/specificMonthsData/"+e+"/"+t)}}return e.\u0275fac=function(t){return new(t||e)(r.Yb(b.b),r.Yb(h.a))},e.\u0275prov=r.Kb({token:e,factory:e.\u0275fac,providedIn:"root"}),e})()},Y0ND:function(e,t,s){"use strict";s.r(t);var n=s("ofXK"),i=s("CLZf"),c=s("tyNb"),a=s("wd/R"),r=s("fXoL"),b=s("0xaD"),h=s("BI3K"),o=s("sYmb"),d=s("jhN1"),p=s("FNUI"),g=s("2mTk");function x(e,t){1&e&&(r.Ub(0,"div",2),r.Ub(1,"div",3),r.Ub(2,"div",4),r.Pb(3,"div",5),r.Tb(),r.Ub(4,"div",4),r.Pb(5,"div",5),r.Tb(),r.Tb(),r.Tb())}function l(e,t){if(1&e&&(r.Ub(0,"tr",41),r.Ub(1,"td",42),r.Dc(2),r.Tb(),r.Ub(3,"td",43),r.Dc(4),r.Tb(),r.Ub(5,"td",44),r.Dc(6),r.Tb(),r.Ub(7,"td",45),r.Dc(8),r.Tb(),r.Ub(9,"td",46),r.Dc(10),r.Tb(),r.Tb()),2&e){const e=t.$implicit,s=r.gc(4);r.Cb(2),r.Ec(e.userName),r.Cb(2),r.Ec(e.expenseTitle),r.Cb(2),r.Ec(e.expenseDescription),r.Cb(2),r.Ec(s.getFormat(e.creationDate)),r.Cb(2),r.Ec(e.moneySpent)}}function u(e,t){if(1&e&&(r.Ub(0,"div",23),r.Ub(1,"div",24),r.Ub(2,"div",25),r.Ub(3,"div",26),r.Pb(4,"div",27),r.Tb(),r.Ub(5,"div",28),r.Ub(6,"h4",29),r.Dc(7),r.hc(8,"translate"),r.Tb(),r.Tb(),r.Ub(9,"div",30),r.Ub(10,"div",31),r.Ub(11,"table",32),r.Ub(12,"thead",33),r.Ub(13,"tr",34),r.Ub(14,"th",35),r.Dc(15),r.hc(16,"translate"),r.Tb(),r.Ub(17,"th",36),r.Dc(18),r.hc(19,"translate"),r.Tb(),r.Ub(20,"th",37),r.Dc(21),r.hc(22,"translate"),r.Tb(),r.Ub(23,"th",38),r.Dc(24),r.hc(25,"translate"),r.Tb(),r.Ub(26,"th",39),r.Dc(27),r.hc(28,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Ub(29,"tbody"),r.Cc(30,l,11,5,"tr",40),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb()),2&e){const e=r.gc(3);r.Cb(7),r.Fc("",r.ic(8,7,"ExpenseCategory."+e.first.categoryName)," "),r.Cb(8),r.Ec(r.ic(16,9,"Expenses.User")),r.Cb(3),r.Ec(r.ic(19,11,"Expenses.Title")),r.Cb(3),r.Ec(r.ic(22,13,"Expenses.Details")),r.Cb(3),r.Ec(r.ic(25,15,"Expenses.Date")),r.Cb(3),r.Ec(r.ic(28,17,"Currency."+e.walletCurrency+"Sign")),r.Cb(3),r.mc("ngForOf",e.first.expenses)}}function E(e,t){if(1&e&&(r.Ub(0,"tr",41),r.Ub(1,"td",42),r.Dc(2),r.Tb(),r.Ub(3,"td",43),r.Dc(4),r.Tb(),r.Ub(5,"td",44),r.Dc(6),r.Tb(),r.Ub(7,"td",45),r.Dc(8),r.Tb(),r.Ub(9,"td",46),r.Dc(10),r.Tb(),r.Tb()),2&e){const e=t.$implicit,s=r.gc(4);r.Cb(2),r.Ec(e.userName),r.Cb(2),r.Ec(e.expenseTitle),r.Cb(2),r.Ec(e.expenseDescription),r.Cb(2),r.Ec(s.getFormat(e.creationDate)),r.Cb(2),r.Ec(e.moneySpent)}}function T(e,t){if(1&e&&(r.Ub(0,"div",47),r.Ub(1,"div",24),r.Ub(2,"div",25),r.Ub(3,"div",26),r.Pb(4,"div",48),r.Tb(),r.Ub(5,"div",28),r.Ub(6,"h4",29),r.Dc(7),r.hc(8,"translate"),r.Tb(),r.Tb(),r.Ub(9,"div",30),r.Ub(10,"div",31),r.Ub(11,"table",32),r.Ub(12,"thead",33),r.Ub(13,"tr",34),r.Ub(14,"th",35),r.Dc(15),r.hc(16,"translate"),r.Tb(),r.Ub(17,"th",36),r.Dc(18),r.hc(19,"translate"),r.Tb(),r.Ub(20,"th",37),r.Dc(21),r.hc(22,"translate"),r.Tb(),r.Ub(23,"th",38),r.Dc(24),r.hc(25,"translate"),r.Tb(),r.Ub(26,"th",39),r.Dc(27),r.hc(28,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Ub(29,"tbody"),r.Cc(30,E,11,5,"tr",40),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb()),2&e){const e=r.gc(3);r.Cb(7),r.Fc("",r.ic(8,7,"ExpenseCategory."+e.second.categoryName)," "),r.Cb(8),r.Ec(r.ic(16,9,"Expenses.User")),r.Cb(3),r.Ec(r.ic(19,11,"Expenses.Title")),r.Cb(3),r.Ec(r.ic(22,13,"Expenses.Details")),r.Cb(3),r.Ec(r.ic(25,15,"Expenses.Date")),r.Cb(3),r.Ec(r.ic(28,17,"Currency."+e.walletCurrency+"Sign")),r.Cb(3),r.mc("ngForOf",e.second.expenses)}}function U(e,t){if(1&e&&(r.Ub(0,"tr",41),r.Ub(1,"td",42),r.Dc(2),r.Tb(),r.Ub(3,"td",43),r.Dc(4),r.Tb(),r.Ub(5,"td",44),r.Dc(6),r.Tb(),r.Ub(7,"td",45),r.Dc(8),r.Tb(),r.Ub(9,"td",46),r.Dc(10),r.Tb(),r.Tb()),2&e){const e=t.$implicit,s=r.gc(4);r.Cb(2),r.Ec(e.userName),r.Cb(2),r.Ec(e.expenseTitle),r.Cb(2),r.Ec(e.expenseDescription),r.Cb(2),r.Ec(s.getFormat(e.creationDate)),r.Cb(2),r.Ec(e.moneySpent)}}function y(e,t){if(1&e&&(r.Ub(0,"div",23),r.Ub(1,"div",24),r.Ub(2,"div",25),r.Ub(3,"div",26),r.Pb(4,"div",49),r.Tb(),r.Ub(5,"div",28),r.Ub(6,"h4",29),r.Dc(7),r.hc(8,"translate"),r.Tb(),r.Tb(),r.Ub(9,"div",30),r.Ub(10,"div",31),r.Ub(11,"table",32),r.Ub(12,"thead",33),r.Ub(13,"tr",34),r.Ub(14,"th",35),r.Dc(15),r.hc(16,"translate"),r.Tb(),r.Ub(17,"th",36),r.Dc(18),r.hc(19,"translate"),r.Tb(),r.Ub(20,"th",37),r.Dc(21),r.hc(22,"translate"),r.Tb(),r.Ub(23,"th",38),r.Dc(24),r.hc(25,"translate"),r.Tb(),r.Ub(26,"th",39),r.Dc(27),r.hc(28,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Ub(29,"tbody"),r.Cc(30,U,11,5,"tr",40),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb()),2&e){const e=r.gc(3);r.Cb(7),r.Fc("",r.ic(8,7,"ExpenseCategory."+e.third.categoryName)," "),r.Cb(8),r.Ec(r.ic(16,9,"Expenses.User")),r.Cb(3),r.Ec(r.ic(19,11,"Expenses.Title")),r.Cb(3),r.Ec(r.ic(22,13,"Expenses.Details")),r.Cb(3),r.Ec(r.ic(25,15,"Expenses.Date")),r.Cb(3),r.Ec(r.ic(28,17,"Currency."+e.walletCurrency+"Sign")),r.Cb(3),r.mc("ngForOf",e.third.expenses)}}function v(e,t){if(1&e&&(r.Ub(0,"tr",41),r.Ub(1,"td",42),r.Dc(2),r.Tb(),r.Ub(3,"td",43),r.Dc(4),r.Tb(),r.Ub(5,"td",44),r.Dc(6),r.Tb(),r.Ub(7,"td",45),r.Dc(8),r.Tb(),r.Ub(9,"td",46),r.Dc(10),r.Tb(),r.Tb()),2&e){const e=t.$implicit,s=r.gc(4);r.Cb(2),r.Ec(e.userName),r.Cb(2),r.Ec(e.expenseTitle),r.Cb(2),r.Ec(e.expenseDescription),r.Cb(2),r.Ec(s.getFormat(e.creationDate)),r.Cb(2),r.Ec(e.moneySpent)}}function m(e,t){if(1&e&&(r.Ub(0,"div",47),r.Ub(1,"div",24),r.Ub(2,"div",25),r.Ub(3,"div",26),r.Pb(4,"div",50),r.Tb(),r.Ub(5,"div",28),r.Ub(6,"h4",29),r.Dc(7),r.hc(8,"translate"),r.Tb(),r.Tb(),r.Ub(9,"div",30),r.Ub(10,"div",31),r.Ub(11,"table",32),r.Ub(12,"thead",33),r.Ub(13,"tr",34),r.Ub(14,"th",35),r.Dc(15),r.hc(16,"translate"),r.Tb(),r.Ub(17,"th",36),r.Dc(18),r.hc(19,"translate"),r.Tb(),r.Ub(20,"th",37),r.Dc(21),r.hc(22,"translate"),r.Tb(),r.Ub(23,"th",38),r.Dc(24),r.hc(25,"translate"),r.Tb(),r.Ub(26,"th",39),r.Dc(27),r.hc(28,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Ub(29,"tbody"),r.Cc(30,v,11,5,"tr",40),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb()),2&e){const e=r.gc(3);r.Cb(7),r.Fc("",r.ic(8,7,"ExpenseCategory."+e.fourth.categoryName)," "),r.Cb(8),r.Ec(r.ic(16,9,"Expenses.User")),r.Cb(3),r.Ec(r.ic(19,11,"Expenses.Title")),r.Cb(3),r.Ec(r.ic(22,13,"Expenses.Details")),r.Cb(3),r.Ec(r.ic(25,15,"Expenses.Date")),r.Cb(3),r.Ec(r.ic(28,17,"Currency."+e.walletCurrency+"Sign")),r.Cb(3),r.mc("ngForOf",e.fourth.expenses)}}function f(e,t){if(1&e&&(r.Ub(0,"tr",41),r.Ub(1,"td",42),r.Dc(2),r.Tb(),r.Ub(3,"td",43),r.Dc(4),r.Tb(),r.Ub(5,"td",44),r.Dc(6),r.Tb(),r.Ub(7,"td",45),r.Dc(8),r.Tb(),r.Ub(9,"td",46),r.Dc(10),r.Tb(),r.Tb()),2&e){const e=t.$implicit,s=r.gc(4);r.Cb(2),r.Ec(e.userName),r.Cb(2),r.Ec(e.expenseTitle),r.Cb(2),r.Ec(e.expenseDescription),r.Cb(2),r.Ec(s.getFormat(e.creationDate)),r.Cb(2),r.Ec(e.moneySpent)}}function C(e,t){if(1&e&&(r.Ub(0,"div",23),r.Ub(1,"div",24),r.Ub(2,"div",25),r.Ub(3,"div",26),r.Pb(4,"div",51),r.Tb(),r.Ub(5,"div",28),r.Ub(6,"h4",29),r.Dc(7),r.hc(8,"translate"),r.Tb(),r.Tb(),r.Ub(9,"div",30),r.Ub(10,"div",31),r.Ub(11,"table",32),r.Ub(12,"thead",33),r.Ub(13,"tr",34),r.Ub(14,"th",35),r.Dc(15),r.hc(16,"translate"),r.Tb(),r.Ub(17,"th",36),r.Dc(18),r.hc(19,"translate"),r.Tb(),r.Ub(20,"th",37),r.Dc(21),r.hc(22,"translate"),r.Tb(),r.Ub(23,"th",38),r.Dc(24),r.hc(25,"translate"),r.Tb(),r.Ub(26,"th",39),r.Dc(27),r.hc(28,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Ub(29,"tbody"),r.Cc(30,f,11,5,"tr",40),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb()),2&e){const e=r.gc(3);r.Cb(7),r.Fc("",r.ic(8,7,"ExpenseCategory."+e.fifth.categoryName)," "),r.Cb(8),r.Ec(r.ic(16,9,"Expenses.User")),r.Cb(3),r.Ec(r.ic(19,11,"Expenses.Title")),r.Cb(3),r.Ec(r.ic(22,13,"Expenses.Details")),r.Cb(3),r.Ec(r.ic(25,15,"Expenses.Date")),r.Cb(3),r.Ec(r.ic(28,17,"Currency."+e.walletCurrency+"Sign")),r.Cb(3),r.mc("ngForOf",e.fifth.expenses)}}function D(e,t){if(1&e&&(r.Ub(0,"tr",41),r.Ub(1,"td",42),r.Dc(2),r.Tb(),r.Ub(3,"td",43),r.Dc(4),r.Tb(),r.Ub(5,"td",44),r.Dc(6),r.Tb(),r.Ub(7,"td",45),r.Dc(8),r.Tb(),r.Ub(9,"td",46),r.Dc(10),r.Tb(),r.Tb()),2&e){const e=t.$implicit,s=r.gc(4);r.Cb(2),r.Ec(e.userName),r.Cb(2),r.Ec(e.expenseTitle),r.Cb(2),r.Ec(e.expenseDescription),r.Cb(2),r.Ec(s.getFormat(e.creationDate)),r.Cb(2),r.Ec(e.moneySpent)}}function I(e,t){if(1&e&&(r.Ub(0,"div",47),r.Ub(1,"div",24),r.Ub(2,"div",25),r.Ub(3,"div",26),r.Pb(4,"div",52),r.Tb(),r.Ub(5,"div",28),r.Ub(6,"h4",29),r.Dc(7),r.hc(8,"translate"),r.Tb(),r.Tb(),r.Ub(9,"div",30),r.Ub(10,"div",31),r.Ub(11,"table",32),r.Ub(12,"thead",33),r.Ub(13,"tr",34),r.Ub(14,"th",35),r.Dc(15),r.hc(16,"translate"),r.Tb(),r.Ub(17,"th",36),r.Dc(18),r.hc(19,"translate"),r.Tb(),r.Ub(20,"th",37),r.Dc(21),r.hc(22,"translate"),r.Tb(),r.Ub(23,"th",38),r.Dc(24),r.hc(25,"translate"),r.Tb(),r.Ub(26,"th",39),r.Dc(27),r.hc(28,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Ub(29,"tbody"),r.Cc(30,D,11,5,"tr",40),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb()),2&e){const e=r.gc(3);r.Cb(7),r.Fc("",r.ic(8,7,"ExpenseCategory."+e.sixth.categoryName)," "),r.Cb(8),r.Ec(r.ic(16,9,"Expenses.User")),r.Cb(3),r.Ec(r.ic(19,11,"Expenses.Title")),r.Cb(3),r.Ec(r.ic(22,13,"Expenses.Details")),r.Cb(3),r.Ec(r.ic(25,15,"Expenses.Date")),r.Cb(3),r.Ec(r.ic(28,17,"Currency."+e.walletCurrency+"Sign")),r.Cb(3),r.mc("ngForOf",e.sixth.expenses)}}function N(e,t){if(1&e&&(r.Ub(0,"tr",41),r.Ub(1,"td",42),r.Dc(2),r.Tb(),r.Ub(3,"td",43),r.Dc(4),r.Tb(),r.Ub(5,"td",44),r.Dc(6),r.Tb(),r.Ub(7,"td",45),r.Dc(8),r.Tb(),r.Ub(9,"td",46),r.Dc(10),r.Tb(),r.Tb()),2&e){const e=t.$implicit,s=r.gc(4);r.Cb(2),r.Ec(e.userName),r.Cb(2),r.Ec(e.expenseTitle),r.Cb(2),r.Ec(e.expenseDescription),r.Cb(2),r.Ec(s.getFormat(e.creationDate)),r.Cb(2),r.Ec(e.moneySpent)}}function M(e,t){if(1&e&&(r.Ub(0,"div",23),r.Ub(1,"div",24),r.Ub(2,"div",25),r.Ub(3,"div",26),r.Pb(4,"div",53),r.Tb(),r.Ub(5,"div",28),r.Ub(6,"h4",29),r.Dc(7),r.hc(8,"translate"),r.Tb(),r.Tb(),r.Ub(9,"div",30),r.Ub(10,"div",31),r.Ub(11,"table",32),r.Ub(12,"thead",33),r.Ub(13,"tr",34),r.Ub(14,"th",35),r.Dc(15),r.hc(16,"translate"),r.Tb(),r.Ub(17,"th",36),r.Dc(18),r.hc(19,"translate"),r.Tb(),r.Ub(20,"th",37),r.Dc(21),r.hc(22,"translate"),r.Tb(),r.Ub(23,"th",38),r.Dc(24),r.hc(25,"translate"),r.Tb(),r.Ub(26,"th",39),r.Dc(27),r.hc(28,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Ub(29,"tbody"),r.Cc(30,N,11,5,"tr",40),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb()),2&e){const e=r.gc(3);r.Cb(7),r.Fc("",r.ic(8,7,"ExpenseCategory."+e.seventh.categoryName)," "),r.Cb(8),r.Ec(r.ic(16,9,"Expenses.User")),r.Cb(3),r.Ec(r.ic(19,11,"Expenses.Title")),r.Cb(3),r.Ec(r.ic(22,13,"Expenses.Details")),r.Cb(3),r.Ec(r.ic(25,15,"Expenses.Date")),r.Cb(3),r.Ec(r.ic(28,17,"Currency."+e.walletCurrency+"Sign")),r.Cb(3),r.mc("ngForOf",e.seventh.expenses)}}function S(e,t){if(1&e&&(r.Ub(0,"tr",41),r.Ub(1,"td",42),r.Dc(2),r.Tb(),r.Ub(3,"td",43),r.Dc(4),r.Tb(),r.Ub(5,"td",44),r.Dc(6),r.Tb(),r.Ub(7,"td",45),r.Dc(8),r.Tb(),r.Ub(9,"td",46),r.Dc(10),r.Tb(),r.Tb()),2&e){const e=t.$implicit,s=r.gc(4);r.Cb(2),r.Ec(e.userName),r.Cb(2),r.Ec(e.expenseTitle),r.Cb(2),r.Ec(e.expenseDescription),r.Cb(2),r.Ec(s.getFormat(e.creationDate)),r.Cb(2),r.Ec(e.moneySpent)}}function w(e,t){if(1&e&&(r.Ub(0,"div",47),r.Ub(1,"div",24),r.Ub(2,"div",25),r.Ub(3,"div",26),r.Pb(4,"div",54),r.Tb(),r.Ub(5,"div",28),r.Ub(6,"h4",29),r.Dc(7),r.hc(8,"translate"),r.Tb(),r.Tb(),r.Ub(9,"div",30),r.Ub(10,"div",31),r.Ub(11,"table",32),r.Ub(12,"thead",33),r.Ub(13,"tr",34),r.Ub(14,"th",35),r.Dc(15),r.hc(16,"translate"),r.Tb(),r.Ub(17,"th",36),r.Dc(18),r.hc(19,"translate"),r.Tb(),r.Ub(20,"th",37),r.Dc(21),r.hc(22,"translate"),r.Tb(),r.Ub(23,"th",38),r.Dc(24),r.hc(25,"translate"),r.Tb(),r.Ub(26,"th",39),r.Dc(27),r.hc(28,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Ub(29,"tbody"),r.Cc(30,S,11,5,"tr",40),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb()),2&e){const e=r.gc(3);r.Cb(7),r.Fc("",r.ic(8,7,"ExpenseCategory."+e.eigth.categoryName)," "),r.Cb(8),r.Ec(r.ic(16,9,"Expenses.User")),r.Cb(3),r.Ec(r.ic(19,11,"Expenses.Title")),r.Cb(3),r.Ec(r.ic(22,13,"Expenses.Details")),r.Cb(3),r.Ec(r.ic(25,15,"Expenses.Date")),r.Cb(3),r.Ec(r.ic(28,17,"Currency."+e.walletCurrency+"Sign")),r.Cb(3),r.mc("ngForOf",e.eigth.expenses)}}function P(e,t){if(1&e&&(r.Ub(0,"tr",41),r.Ub(1,"td",42),r.Dc(2),r.Tb(),r.Ub(3,"td",43),r.Dc(4),r.Tb(),r.Ub(5,"td",44),r.Dc(6),r.Tb(),r.Ub(7,"td",45),r.Dc(8),r.Tb(),r.Ub(9,"td",46),r.Dc(10),r.Tb(),r.Tb()),2&e){const e=t.$implicit,s=r.gc(4);r.Cb(2),r.Ec(e.userName),r.Cb(2),r.Ec(e.expenseTitle),r.Cb(2),r.Ec(e.expenseDescription),r.Cb(2),r.Ec(s.getFormat(e.creationDate)),r.Cb(2),r.Ec(e.moneySpent)}}function O(e,t){if(1&e&&(r.Ub(0,"div",23),r.Ub(1,"div",24),r.Ub(2,"div",25),r.Ub(3,"div",26),r.Pb(4,"div",55),r.Tb(),r.Ub(5,"div",28),r.Ub(6,"h4",29),r.Dc(7),r.hc(8,"translate"),r.Tb(),r.Tb(),r.Ub(9,"div",30),r.Ub(10,"div",31),r.Ub(11,"table",32),r.Ub(12,"thead",33),r.Ub(13,"tr",34),r.Ub(14,"th",35),r.Dc(15),r.hc(16,"translate"),r.Tb(),r.Ub(17,"th",36),r.Dc(18),r.hc(19,"translate"),r.Tb(),r.Ub(20,"th",37),r.Dc(21),r.hc(22,"translate"),r.Tb(),r.Ub(23,"th",38),r.Dc(24),r.hc(25,"translate"),r.Tb(),r.Ub(26,"th",39),r.Dc(27),r.hc(28,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Ub(29,"tbody"),r.Cc(30,P,11,5,"tr",40),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb()),2&e){const e=r.gc(3);r.Cb(7),r.Fc("",r.ic(8,7,"ExpenseCategory."+e.nineth.categoryName)," "),r.Cb(8),r.Ec(r.ic(16,9,"Expenses.User")),r.Cb(3),r.Ec(r.ic(19,11,"Expenses.Title")),r.Cb(3),r.Ec(r.ic(22,13,"Expenses.Details")),r.Cb(3),r.Ec(r.ic(25,15,"Expenses.Date")),r.Cb(3),r.Ec(r.ic(28,17,"Currency."+e.walletCurrency+"Sign")),r.Cb(3),r.mc("ngForOf",e.nineth.expenses)}}function F(e,t){if(1&e&&(r.Ub(0,"tr",41),r.Ub(1,"td",42),r.Dc(2),r.Tb(),r.Ub(3,"td",43),r.Dc(4),r.Tb(),r.Ub(5,"td",44),r.Dc(6),r.Tb(),r.Ub(7,"td",45),r.Dc(8),r.Tb(),r.Ub(9,"td",46),r.Dc(10),r.Tb(),r.Tb()),2&e){const e=t.$implicit,s=r.gc(4);r.Cb(2),r.Ec(e.userName),r.Cb(2),r.Ec(e.expenseTitle),r.Cb(2),r.Ec(e.expenseDescription),r.Cb(2),r.Ec(s.getFormat(e.creationDate)),r.Cb(2),r.Ec(e.moneySpent)}}function k(e,t){if(1&e&&(r.Ub(0,"div",47),r.Ub(1,"div",24),r.Ub(2,"div",25),r.Ub(3,"div",26),r.Pb(4,"div",56),r.Tb(),r.Ub(5,"div",28),r.Ub(6,"h4",29),r.Dc(7),r.hc(8,"translate"),r.Tb(),r.Tb(),r.Ub(9,"div",30),r.Ub(10,"div",31),r.Ub(11,"table",32),r.Ub(12,"thead",33),r.Ub(13,"tr",34),r.Ub(14,"th",35),r.Dc(15),r.hc(16,"translate"),r.Tb(),r.Ub(17,"th",36),r.Dc(18),r.hc(19,"translate"),r.Tb(),r.Ub(20,"th",37),r.Dc(21),r.hc(22,"translate"),r.Tb(),r.Ub(23,"th",38),r.Dc(24),r.hc(25,"translate"),r.Tb(),r.Ub(26,"th",39),r.Dc(27),r.hc(28,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Ub(29,"tbody"),r.Cc(30,F,11,5,"tr",40),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb()),2&e){const e=r.gc(3);r.Cb(7),r.Fc("",r.ic(8,7,"ExpenseCategory."+e.tenth.categoryName)," "),r.Cb(8),r.Ec(r.ic(16,9,"Expenses.User")),r.Cb(3),r.Ec(r.ic(19,11,"Expenses.Title")),r.Cb(3),r.Ec(r.ic(22,13,"Expenses.Details")),r.Cb(3),r.Ec(r.ic(25,15,"Expenses.Date")),r.Cb(3),r.Ec(r.ic(28,17,"Currency."+e.walletCurrency+"Sign")),r.Cb(3),r.mc("ngForOf",e.tenth.expenses)}}function j(e,t){if(1&e&&(r.Ub(0,"div",19),r.Ub(1,"div",20),r.Cc(2,u,31,19,"div",21),r.Cc(3,T,31,19,"div",22),r.Cc(4,y,31,19,"div",21),r.Cc(5,m,31,19,"div",22),r.Cc(6,C,31,19,"div",21),r.Cc(7,I,31,19,"div",22),r.Cc(8,M,31,19,"div",21),r.Cc(9,w,31,19,"div",22),r.Cc(10,O,31,19,"div",21),r.Cc(11,k,31,19,"div",22),r.Tb(),r.Tb()),2&e){const e=r.gc(2);r.Cb(2),r.mc("ngIf",0!=e.first.categoryId),r.Cb(1),r.mc("ngIf",0!=e.second.categoryId),r.Cb(1),r.mc("ngIf",0!=e.third.categoryId),r.Cb(1),r.mc("ngIf",0!=e.fourth.categoryId),r.Cb(1),r.mc("ngIf",0!=e.fifth.categoryId),r.Cb(1),r.mc("ngIf",0!=e.sixth.categoryId),r.Cb(1),r.mc("ngIf",0!=e.seventh.categoryId),r.Cb(1),r.mc("ngIf",0!=e.eigth.categoryId),r.Cb(1),r.mc("ngIf",0!=e.nineth.categoryId),r.Cb(1),r.mc("ngIf",0!=e.tenth.categoryId)}}function _(e,t){if(1&e&&(r.Ub(0,"div"),r.Pb(1,"app-pie-graph",66),r.Tb()),2&e){const e=r.gc(3);r.Cb(1),r.mc("topFiveUsers",e.topFiveUsers)}}function L(e,t){if(1&e&&(r.Ub(0,"div"),r.Pb(1,"app-donut-chart-categories",67),r.Tb()),2&e){const e=r.gc(3);r.Cb(1),r.mc("categories",e.categories)("barExpensesList",e.barExpenses)}}function Y(e,t){if(1&e&&(r.Ub(0,"div",57),r.Ub(1,"div",58),r.Ub(2,"div",28),r.Ub(3,"h5",59),r.Dc(4),r.hc(5,"translate"),r.Tb(),r.Ub(6,"p",60),r.Dc(7),r.Tb(),r.Tb(),r.Ub(8,"div",30),r.Cc(9,_,2,1,"div",1),r.Tb(),r.Ub(10,"div",61),r.Pb(11,"hr"),r.Ub(12,"div",62),r.Pb(13,"i",63),r.Dc(14),r.hc(15,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Ub(16,"div",64),r.Ub(17,"div",28),r.Ub(18,"h5",59),r.Dc(19),r.hc(20,"translate"),r.Tb(),r.Ub(21,"p",60),r.Dc(22),r.Tb(),r.Tb(),r.Ub(23,"div",30),r.Cc(24,L,2,2,"div",1),r.Tb(),r.Ub(25,"div",61),r.Pb(26,"hr"),r.Ub(27,"div",62),r.Pb(28,"i",65),r.Dc(29),r.hc(30,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Tb()),2&e){const e=r.gc(2);r.Cb(4),r.Ec(r.ic(5,8,"Expenses.Top")),r.Cb(3),r.Ec(e.monthName),r.Cb(2),r.mc("ngIf",null!=e.topFiveUsers),r.Cb(5),r.Fc(" ",r.ic(15,10,"Footer.Top5")," "),r.Cb(5),r.Ec(r.ic(20,12,"Expenses.CategoryCompare")),r.Cb(3),r.Ec(e.monthName),r.Cb(2),r.mc("ngIf",null!=e.barExpenses&&e.categories.length>0),r.Cb(5),r.Fc(" ",r.ic(30,14,"Footer.CatComp")," ")}}function z(e,t){if(1&e&&(r.Ub(0,"div",68),r.Ub(1,"div",69),r.Ub(2,"div",70),r.Ub(3,"div",71),r.Ub(4,"div",30),r.Ub(5,"div",15),r.Ub(6,"div",72),r.Ub(7,"div",73),r.Pb(8,"i",74),r.Tb(),r.Tb(),r.Ub(9,"div",75),r.Ub(10,"div",76),r.Ub(11,"p",59),r.Dc(12),r.hc(13,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Ub(14,"div",61),r.Pb(15,"hr"),r.Ub(16,"div",62),r.Pb(17,"i",77),r.Dc(18),r.hc(19,"translate"),r.hc(20,"translate"),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb(),r.Tb()),2&e){const e=r.gc(2);r.Cb(12),r.Fc("",r.ic(13,5,"Expenses.Nothing")," "),r.Cb(6),r.Hc(" ",r.ic(19,7,"Expenses.Nothing")," ",r.ic(20,9,"Expenses.On")," ",e.monthName,", ",e.year," ")}}function $(e,t){if(1&e){const e=r.Vb();r.Ub(0,"div"),r.Ub(1,"div",6),r.Ub(2,"div",7),r.Ub(3,"div",8),r.Ub(4,"div",9),r.cc("click",(function(){return r.vc(e),r.gc().previousMonth()})),r.Pb(5,"i",10),r.Tb(),r.Ub(6,"div",11),r.Ub(7,"div",12),r.Dc(8),r.Tb(),r.Tb(),r.Ub(9,"div",13),r.cc("click",(function(){return r.vc(e),r.gc().next()})),r.Pb(10,"i",14),r.Tb(),r.Tb(),r.Tb(),r.Ub(11,"div",15),r.Cc(12,j,12,10,"div",16),r.Cc(13,Y,31,16,"div",17),r.Cc(14,z,21,11,"div",18),r.Tb(),r.Tb(),r.Tb()}if(2&e){const e=r.gc();r.Cb(8),r.Gc("",e.monthName,", ",e.year,""),r.Cb(4),r.mc("ngIf",e.topFiveUsers.length>0),r.Cb(1),r.mc("ngIf",e.topFiveUsers.length>0),r.Cb(1),r.mc("ngIf",0===e.topFiveUsers.length&&!e.isLoading)}}let V=(()=>{class e{constructor(e,t,s,n){this.expenseService=e,this.walletService=t,this.translateService=s,this.titleService=n,this.first={categoryName:"",expenses:[],categoryId:0},this.second={categoryName:"",expenses:[],categoryId:0},this.third={categoryName:"",expenses:[],categoryId:0},this.fourth={categoryName:"",expenses:[],categoryId:0},this.fifth={categoryName:"",expenses:[],categoryId:0},this.sixth={categoryName:"",expenses:[],categoryId:0},this.seventh={categoryName:"",expenses:[],categoryId:0},this.eigth={categoryName:"",expenses:[],categoryId:0},this.nineth={categoryName:"",expenses:[],categoryId:0},this.tenth={categoryName:"",expenses:[],categoryId:0},this.isLoading=!0,this.categories=[],this.walletCurrency="USD",this.monthNumber=1,this.monthName=""}ngOnInit(){this.isLoading=!0,"en"===this.translateService.currentLang?a.locale("en"):"ru"===this.translateService.currentLang&&a.locale("ru"),this.translateService.onLangChange.subscribe(()=>{"en"===this.translateService.currentLang?a.locale("en"):"ru"===this.translateService.currentLang&&a.locale("ru"),this.monthName=a(this.date).format("MMMM")}),this.date=new Date(Date.now()),this.date.setMonth(this.date.getMonth()-1),this.year=a(this.date).format("YYYY"),console.log("Init month",this.date),this.monthName=a(this.date).format("MMMM"),0===this.walletService.currentCategories.length?this.walletService.getWalletsCategories().subscribe(e=>{this.walletService.currentCategories=e,this.categories=this.walletService.currentCategories}):this.categories=this.walletService.currentCategories,this.walletService.getCurrentWallet().subscribe(e=>{this.walletCurrency=e.currency}),this.getData(this.date),this.setTitle(this.translateService.currentLang),this.translateService.onLangChange.subscribe(e=>{this.setTitle(e.lang)})}setTitle(e){"en"===e?this.titleService.setTitle("Previous Expenses"):"ru"===e&&this.titleService.setTitle("\u041f\u0440\u043e\u0448\u043b\u044b\u0435 \u0422\u0440\u0430\u0442\u044b")}getData(e){this.expenseService.getPreviousExpenses(e.toUTCString()).subscribe(e=>{this.isLoading=!0,this.barExpenses=e.previousExpensesBars,this.topFiveUsers=e.topFiveUsers,this.topFiveUsers.length>0&&(e.previousMonthExpenses[0].expenses.length>0&&(this.first.expenses=e.previousMonthExpenses[0].expenses,this.first.categoryId=e.previousMonthExpenses[0].categoryId,this.first.categoryName=e.previousMonthExpenses[0].categoryName),e.previousMonthExpenses[1].expenses.length>0&&(this.second.expenses=e.previousMonthExpenses[1].expenses,this.second.categoryId=e.previousMonthExpenses[1].categoryId,this.second.categoryName=e.previousMonthExpenses[1].categoryName),e.previousMonthExpenses[2].expenses.length>0&&(this.third.expenses=e.previousMonthExpenses[2].expenses,this.third.categoryId=e.previousMonthExpenses[2].categoryId,this.third.categoryName=e.previousMonthExpenses[2].categoryName),e.previousMonthExpenses[3].expenses.length>0&&(this.fourth.expenses=e.previousMonthExpenses[3].expenses,this.fourth.categoryId=e.previousMonthExpenses[3].categoryId,this.fourth.categoryName=e.previousMonthExpenses[3].categoryName),e.previousMonthExpenses[4].expenses.length>0&&(this.fifth.expenses=e.previousMonthExpenses[4].expenses,this.fifth.categoryId=e.previousMonthExpenses[4].categoryId,this.fifth.categoryName=e.previousMonthExpenses[4].categoryName),e.previousMonthExpenses.length>5&&(e.previousMonthExpenses[5].expenses.length>0&&(this.sixth.expenses=e.previousMonthExpenses[5].expenses,this.sixth.categoryId=e.previousMonthExpenses[5].categoryId,this.sixth.categoryName=e.previousMonthExpenses[5].categoryName),e.previousMonthExpenses.length>6&&(e.previousMonthExpenses[6].expenses.length>0&&(this.seventh.expenses=e.previousMonthExpenses[6].expenses,this.seventh.categoryId=e.previousMonthExpenses[6].categoryId,this.seventh.categoryName=e.previousMonthExpenses[6].categoryName),e.previousMonthExpenses.length>7&&(e.previousMonthExpenses[7].expenses.length>0&&(this.eigth.expenses=e.previousMonthExpenses[7].expenses,this.eigth.categoryId=e.previousMonthExpenses[7].categoryId,this.eigth.categoryName=e.previousMonthExpenses[7].categoryName),e.previousMonthExpenses.length>8&&(e.previousMonthExpenses[8].expenses.length>0&&(this.nineth.expenses=e.previousMonthExpenses[8].expenses,this.nineth.categoryId=e.previousMonthExpenses[8].categoryId,this.nineth.categoryName=e.previousMonthExpenses[8].categoryName),e.previousMonthExpenses.length>9&&e.previousMonthExpenses[9].expenses.length>0&&(this.tenth.expenses=e.previousMonthExpenses[9].expenses,this.tenth.categoryId=e.previousMonthExpenses[9].categoryId,this.tenth.categoryName=e.previousMonthExpenses[9].categoryName)))))),this.isLoading=!1})}previousMonth(){this.isLoading=!0,this.date=new Date(Date.now()),this.monthNumber++,this.date.setMonth(this.date.getMonth()-this.monthNumber,1),console.log("should be first day of previous month",this.date),this.monthName=a(this.date).format("MMMM"),this.year=a(this.date).format("YYYY"),this.clearData(),this.getData(this.date)}next(){this.isLoading=!0,this.monthNumber-1!=0&&(this.monthNumber--,this.date=new Date(Date.now()),this.date.setMonth(this.date.getMonth()-this.monthNumber,1),this.monthName=a(this.date).format("MMMM"),this.year=a(this.date).format("YYYY"),this.clearData(),this.getData(this.date))}clearData(){this.isLoading=!0,this.first={categoryName:"",expenses:[],categoryId:0},this.second={categoryName:"",expenses:[],categoryId:0},this.third={categoryName:"",expenses:[],categoryId:0},this.fourth={categoryName:"",expenses:[],categoryId:0},this.fifth={categoryName:"",expenses:[],categoryId:0},this.sixth={categoryName:"",expenses:[],categoryId:0},this.seventh={categoryName:"",expenses:[],categoryId:0},this.eigth={categoryName:"",expenses:[],categoryId:0},this.nineth={categoryName:"",expenses:[],categoryId:0},this.tenth={categoryName:"",expenses:[],categoryId:0},this.topFiveUsers=[],this.barExpenses=null}getFormat(e){return a(e).format("lll")}}return e.\u0275fac=function(t){return new(t||e)(r.Ob(b.a),r.Ob(h.a),r.Ob(o.d),r.Ob(d.d))},e.\u0275cmp=r.Ib({type:e,selectors:[["app-show-previous-expenses"]],decls:2,vars:2,consts:[["class","loader-wrapper",4,"ngIf"],[4,"ngIf"],[1,"loader-wrapper"],[1,"loader"],[1,"face"],[1,"circle"],[1,"container-fluid","mr-5"],["data-aos","fade-down","data-aos-duration","1200","data-aos-once","true","data-aos-delay","0",1,"card"],[1,"header"],[1,"previous-day",3,"click"],[1,"fas","fa-chevron-left","left","ml-3",2,"font-size","60px"],[1,"date"],[1,"date-value","text-capitalize"],[1,"next-day",3,"click"],[1,"fas","fa-chevron-right","right","mr-3",2,"font-size","60px"],[1,"row"],["class","col-xl-8 col-md-12 no-pad",4,"ngIf"],["class","col-xl-4 col-md-12 no-pad",4,"ngIf"],["class","col-12",4,"ngIf"],[1,"col-xl-8","col-md-12","no-pad"],[1,"content"],["class","row","data-aos","fade-left","data-aos-duration","1200","data-aos-once","true","data-aos-delay","100",4,"ngIf"],["class","row","data-aos","fade-right","data-aos-duration","1200","data-aos-once","true","data-aos-delay","200",4,"ngIf"],["data-aos","fade-left","data-aos-duration","1200","data-aos-once","true","data-aos-delay","100",1,"row"],[1,"col-md-12","no-pad"],[1,"card","card-img"],[1,"image"],[2,"width","100%","height","100%","background-color","#cafcfa"],[1,"card-header"],[1,"card-title","text-center","font-weight-bold"],[1,"card-body"],[1,"table-responsive"],[1,"table"],[1,"text-primary","text-center","table-head"],[1,"d-flex","text-center"],[1,"user-header"],[1,"title-header"],[1,"details-header"],[1,"date-header"],[1,"money-header"],["class","d-flex text-center table-text",4,"ngFor","ngForOf"],[1,"d-flex","text-center","table-text"],[1,"user-cell"],[1,"title-cell"],[1,"details-cell"],[1,"date-cell"],[1,"money-cell"],["data-aos","fade-right","data-aos-duration","1200","data-aos-once","true","data-aos-delay","200",1,"row"],[2,"width","100%","height","100%","background-color","#cafcdf"],[2,"width","100%","height","100%","background-color","#dafcca"],[2,"width","100%","height","100%","background-color","#fcf0ca"],[2,"width","100%","height","100%","background-color","#fcd9ca"],[2,"width","100%","height","100%","background-color","#fccaca"],[2,"width","100%","height","100%","background-color","#fccaea"],[2,"width","100%","height","100%","background-color","#eccafc"],[2,"width","100%","height","100%","background-color","#d1cafc"],[2,"width","100%","height","100%","background-color","#cad8fc"],[1,"col-xl-4","col-md-12","no-pad"],["data-aos","fade-right","data-aos-duration","1200","data-aos-once","true","data-aos-delay","0",1,"card",2,"border-radius","12px"],[1,"card-title"],[1,"card-category"],[1,"card-footer"],[1,"stats"],[1,"fa","fa-money"],["data-aos","fade-left","data-aos-duration","1200","data-aos-once","true","data-aos-delay","200",1,"card",2,"border-radius","12px"],[1,"fas","fa-chart-pie"],[3,"topFiveUsers"],[3,"categories","barExpensesList"],[1,"col-12"],[1,"row","d-flex","justify-content-center"],[1,"col-lg-3","col-md-6","col-sm-6"],[1,"card","card-stats"],[1,"col-5","col-md-4"],[1,"icon-big","text-center","icon-warning"],[1,"fa","fa-exclamation-triangle","text-warning"],[1,"col-7","col-md-8"],[1,"numbers"],[1,"fa","fa-refresh"]],template:function(e,t){1&e&&(r.Cc(0,x,6,0,"div",0),r.Cc(1,$,15,5,"div",1)),2&e&&(r.mc("ngIf",t.isLoading),r.Cb(1),r.mc("ngIf",!t.isLoading))},directives:[n.l,n.k,p.a,g.a],pipes:[o.c],styles:[".image[_ngcontent-%COMP%]   div[_ngcontent-%COMP%]{opacity:var(--table-expense-header)}.header[_ngcontent-%COMP%]{display:flex;align-items:center;justify-content:space-between;background-color:var(--card-background);border-radius:8px}.next-day[_ngcontent-%COMP%], .previous-day[_ngcontent-%COMP%]{width:20%;height:80px;border-radius:8px}.next-day[_ngcontent-%COMP%]{justify-content:flex-end}.next-day[_ngcontent-%COMP%], .previous-day[_ngcontent-%COMP%]{display:flex;align-items:center}.next-day[_ngcontent-%COMP%]   .right[_ngcontent-%COMP%], .previous-day[_ngcontent-%COMP%]   .left[_ngcontent-%COMP%]{color:var(--sidebar-item-selected)}.previous-day[_ngcontent-%COMP%]:hover{background:linear-gradient(90deg,var(--sidebar-item-selected) 25%,var(--card-background))}.next-day[_ngcontent-%COMP%]:hover, .next-day[_ngcontent-%COMP%]:hover   .right[_ngcontent-%COMP%], .previous-day[_ngcontent-%COMP%]:hover   .left[_ngcontent-%COMP%]{color:var(--card-background)}.next-day[_ngcontent-%COMP%]:hover{background:linear-gradient(270deg,var(--sidebar-item-selected),var(--card-background))}.date[_ngcontent-%COMP%]{display:flex;align-items:center}.date-value[_ngcontent-%COMP%]{font-size:40px;color:var(--sidebar-item-selected)}@media (max-width:992px){.date-cell[_ngcontent-%COMP%], .date-header[_ngcontent-%COMP%]{width:27%}}@media (max-width:570px){.date-value[_ngcontent-%COMP%]{font-size:30px!important}.left[_ngcontent-%COMP%], .right[_ngcontent-%COMP%]{font-size:40px!important}}@media (max-width:500px){.no-pad[_ngcontent-%COMP%]{padding-right:0!important;padding-left:0!important}}@media (max-width:465px){.date-value[_ngcontent-%COMP%]{font-size:20px!important}.left[_ngcontent-%COMP%], .right[_ngcontent-%COMP%]{font-size:30px!important}}@media (max-width:445px){.table-head[_ngcontent-%COMP%], .table-text[_ngcontent-%COMP%]{font-size:12px!important}}@media (max-width:405px){.table-head[_ngcontent-%COMP%], .table-text[_ngcontent-%COMP%]{font-size:10px!important}}@media (max-width:373px){.table-head[_ngcontent-%COMP%], .table-text[_ngcontent-%COMP%]{font-size:9px!important}}@media (max-width:350px){.table-head[_ngcontent-%COMP%]{font-size:7px!important}.table-text[_ngcontent-%COMP%]{font-size:8px!important}}"]}),e})();var W=s("BLxY");s.d(t,"PreviousExpensesModule",(function(){return X}));let X=(()=>{class e{}return e.\u0275mod=r.Mb({type:e}),e.\u0275inj=r.Lb({factory:function(t){return new(t||e)},imports:[[n.c,c.f.forChild([{path:"",component:V}]),i.a,W.a]]}),e})()}}]);