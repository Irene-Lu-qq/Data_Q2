# Data_Q2

# 資料處理與呈現

建立一個 ASP.NET MVC 專案，顯示資料計算結果


## 基本

- 資料來源為 data1.json ~ data3.json

- 執行 http:{host}/Dashboard，隨機呈現 data1 ~ data3 其中一個資料統計結果

- 呈現統計資料欄位

    - Ticket - Ticket 數量總和
    - AvgResponseMinutes - 平均回應時間(分鐘), 取到整數，轉成下列格式(符合其中一種)
    
        - Day - 天，取最小天數 ex: 1440 min -> 1 day , 3000 min -> 2 day
        - Hour - 小時，需要包含小數點 ex: 90 min = 1.5 hr
        - Minute - ex: 30 min -> 30 min

- UI 樣式不拘，可以自己設計
    

## 改良

- 無