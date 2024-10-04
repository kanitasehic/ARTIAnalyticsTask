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
1. How would you scale this (or similar) solution to support large datasets (range of tens of GBs) for training?
- Consider parallelizing the training process and eliminating features that do not provide significant value. In this example, I lacked knowledge about the features' nature and did not perform an in-depth analysis. Complex data types may need normalization.
2. How would you scale this (or similar) solution to support large datasets (range of tens of GBs) through APIs?
- Consider the idea of using NoSQL databases, as they tend to be more flexible and scalable when dealing with large amounts of data.
3. How would you scale this (or similar) solution to support large number of API calls?
- Load balancers (horizontal scaling) can be implemented to distribute incoming requests across multiple instances of the same app service
Caching can be beneficial in many situations, especially when reading data from databases that store vast amounts of information, as it can speed up the API response times.
4. Both models are having same label variable. Why is one of them better than another one?
- Since I do not know the nature of the data I was forecasting, I cannot definitively say which model produced better results. Statistically, XGBoost seems to be better. Both models can be effectively utilized depending on the purpose they serve.
From a technical standpoint, XGBoost proved to be faster, and likely more efficient for larger datasets. Everything else would depend on the type of problem being solved and the data at hand.
