# BudgetService
refactor training precondition
please complete this kata to attend this training

please develop a budget query system and give me your github link

specification by example.
---
1. input two datetime to query TotalAmount();
2. the budget is set to hole month, 
   - ex: 300 for "201804", 31 for "201805"
3. need to handle startDate bigger than endDate,
   - ex: 300 for "201804", query 2018/04/30" to 2018/04/01, return 0
4. Amount query is by days. 
   - ex: 300 for "201804", query date is 2018/04/01 to 2018/04/02, the result is 20
5. if not set budget, return 0, 
   - ex: 300 for "201804", query date is 2018/03/01 to 2018/03/05, the result is 0
6. if query cross month, should sum each month's budget, 
   - ex: 31 for "201805", 300 for "201804", query date is 2018/04/30 to 2018/05/02, the result is 10+2=12

you might need to create a fake repository to pass test. because the data repository is not implement yet.
<br>
the days in month is according to real world setting, ex: 30 days for "201804", 28 days for "201802"
<br>
assume the budget could be divisible, no need to consider decimal point.
<br>
<br>
if any question need to clarify, please reach me thanks

restrict:
---
- class BudgetService<br>
+TotalAmount(DateTime, DateTime):double

- class Budget<br>
+YearMonth:string<br>
+Amount:int

- class BudgetRepository:IBudgetRepository<br>
+GetAll()
