from sklearn import tree
import pandas as pd
import numpy as np

data = pd.read_csv('question-data-generated.csv')

print(data.head())


data_list = data.values.tolist()

X = []
y = []

#for i in range(len(data_list)):
for i in range(10):
    X.append(data_list[i][:-1])
    y.append(data_list[i][len(data_list[i])-1])

for i in range(10):
    print(X[i])
    print(y[i])
