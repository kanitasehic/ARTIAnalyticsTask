# ARTIAnalyticsTask
This project implements machine learning models using XGBoost and ARIMA for forecasting tasks. Below is an explanation of how to run both projects, the implementation details, and scalability considerations.

## Getting Started

To test the task, clone this project into Visual Studio and run it.

### Setting Up the Python Environment

1. Open Visual Studio Code and clone the following repository: https://github.com/kanitasehic/MachineLearningModelsAPI.git
2. Create a Python environment and execute the following command in the terminal (This will install all the necessary packages to run the API):

   ```bash
   py -m pip install -r requirements.txt
3. Start the API with the command:
   ```bash
   py app.py
4. After that, test the task results through Swagger in the .NET API.

## Implementation of Machine Learning Models
For the XGBoost model, some preprocessing was required before training due to approximately 10,000 rows with null values. Various methods were tested, such as deleting these rows, mean and median imputation, with KNNImputer providing the best results.

After some research, I concluded that the ARIMA model is very effective for time series data, so this model was trained as well.

Once the models were created, they were exported. The whole process can be found at the following link: https://colab.research.google.com/drive/1ENGbjVcjnNIEaiE_BuvyO1WsWPY4bqNm?usp=sharing

## Setting Up API

To create an API for testing using .NET, I considered two options:
1. Fully embedding the exported models in the .NET code
2. Creating a Python API that can be called from .NET service

The first option involved using the Python.NET NuGet package, which turned out not to be the best choice for several reasons:

- This package depends on the Python version and the DLL file located on the local machine, requiring a specified path to python.dll
- This complicates testing on other machines and necessitates several configurations before starting the API
- Furthermore, any ML model changes would require changes in .NET project
- In my opinion, the model should be used as an external forecasting provider
- Generally, mixing two programming languages is not a good idea, as various obstacles are encountered

Consequently, I created a simple API in Python, specifically using the Flask micro-framework. This way, any modifications or improvements to the model require changes in only one project, rather than all other projects that consume that model.

## Solution Analysis

### XGBoost Model Results

The XGBoost model was trained using a regression dataset and produced the following results:

- **Mean Absolute Error (MAE):** 4.3997
- **Mean Squared Error (MSE):** 392.8823
- **Root Mean Squared Error (RMSE):** 19.8213
- **R-squared:** 0.9609

#### Interpretation
- The **MAE** indicates that, on average, the model's predictions are about 4.40 units away from the actual values.
- The **MSE** and **RMSE** values suggest that the model performs well with relatively low error.
- An **R-squared** value of 0.9609 implies that approximately 96.09% of the variance in the label variable is explained by the model, indicating a strong predictive capability.

### ARIMA Model Results

The ARIMA model was trained using a time-series dataset and produced the following results:

- **Mean Absolute Error (MAE):** 53.9582
- **Mean Squared Error (MSE):** 17417.6325
- **Root Mean Squared Error (RMSE):** 131.9759
- **R-squared:** -0.0162

#### Interpretation
- The **MAE** shows that the model's predictions deviate by an average of 53.96 units from the actual values, indicating significantly higher error compared to the XGBoost model.
- The **MSE** and **RMSE** values are much larger, reflecting a poorer predictive performance.
- The **R-squared** value of -0.0162 suggests that the model does not explain the variance in the label variable well, which is a clear indicator of underperformance.

### Comparison

Both models were trained on datasets with the same label variable, which has a minimum value of 0 and a maximum value of 874.08. The performance metrics highlight the following:

- **XGBoost** demonstrates a substantially better performance across all metrics compared to the **ARIMA** model. 
- The lower error rates and higher R-squared value of XGBoost indicate its effectiveness in capturing the relationship between features and the label variable, whereas ARIMA struggled significantly in this regard.

### Conclusion

Based on the comparison, it is evident that the XGBoost model outperforms the ARIMA model for this particular task and dataset. The statistical properties of XGBoost make it a more suitable choice for this scenario, likely due to its ability to handle non-linear relationships and interactions between features more effectively.

1. How would you scale this (or similar) solution to support large datasets (range of tens of GBs) for training?
- Consider parallelizing the training process and eliminating features that do not provide significant value. In this example, I lacked knowledge about the features' nature and did not perform an in-depth analysis. Complex data types may need normalization.
2. How would you scale this (or similar) solution to support large datasets (range of tens of GBs) through APIs?
- Consider the idea of using NoSQL databases, as they tend to be more flexible and scalable when dealing with large amounts of data.
3. How would you scale this (or similar) solution to support large number of API calls?
- Load balancers (horizontal scaling) can be implemented to distribute incoming requests across multiple instances of the same app service
Caching can be beneficial in many situations, especially when reading data from databases that store vast amounts of information, as it can speed up the API response times.
4. Both models are having same label variable. Why is one of them better than another one?
- From a technical standpoint, XGBoost proved to be faster, and likely more efficient for larger datasets. Given that both models utilize the same label variable but with different characteristics of the datasets, the choice of model may ultimately depend on the specific context and the nature of the data being analyzed. However, based on the provided metrics, the XGBoost model is a clear choice for this task due to its robust predictive capabilities. Ultimately, the ARIMA model may not be the most suitable choice for this specific time-series dataset.
