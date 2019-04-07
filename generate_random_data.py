import random

needy_vals = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
goodjob_vals = [0, 1, 2, 3, 4]
military_vals = [2, 0, -2]
wealth_vals = [0, 1, 2, 3]
regulation_vals = [0, 1, 2, 3, 4]


all_data = []
all_calc_sum = 0


count = 0
min_calc = 5
max_calc = 5

csv_header = "needyHours,goodjob0,goodjob1,goodjob2,goodjob3,goodjob4,Breakfast0,Breakfast1,Breakfast2,Breakfast3,regulationical0,regulationical1,regulationical2,regulationical3,regulationical4,military0,military1,military2,Happy0,Happy1,Happy2,Happy3,Happy4,RATING"

while (count < 2000):

    current_row = []

    needy_val = needy_vals[random.randint(0,12)]
    current_row.append(needy_val)

    if (needy_val < 3):
        goodjob_val = goodjob_vals[random.randint(0, 1)]
    elif (3 <= needy_val < 5):
        goodjob_val = goodjob_vals[random.randint(0, 2)]
    elif (5 <= needy_val < 9):
        goodjob_val = goodjob_vals[random.randint(2, 4)]
    else:
        goodjob_val = goodjob_vals[random.randint(3, 4)]
        
    for i in range(5):
        if i != goodjob_val:
            current_row.append(0)
        elif i == goodjob_val:
            current_row.append(1)

    military_val = military_vals[random.randint(0, 2)]

    military_plus_goodjob = military_val + goodjob_val

    if (military_plus_goodjob >= 2):
        wealth_val = wealth_vals[random.randint(0, 2)]
    else:
        wealth_val = wealth_vals[random.randint(2, 3)]

    for i in range(4):
        if i != wealth_val:
            current_row.append(0)
        elif i == wealth_val:
            current_row.append(1)

    military_plus_needyHours = (2*military_val) + needy_val

    if (military_plus_needyHours <= 0):
        regulation_val = regulation_vals[random.randint(0,1)]
    elif (0 <= military_plus_needyHours < 4):
        regulation_val = regulation_vals[random.randint(1,3)]
    elif (4 <= military_plus_needyHours < 9):
        regulation_val = regulation_vals[random.randint(2,4)]
    elif (military_plus_needyHours >= 9):
        regulation_val = regulation_vals[random.randint(3,4)]

    for i in range(5):
        if i != regulation_val:
            current_row.append(0)
        elif i == regulation_val:
            current_row.append(1)

    if (military_val == 2):
        current_row.extend([1, 0, 0])
    elif (military_val == 0):
        current_row.extend([0, 1, 0])
    elif (military_val == -2):
        current_row.extend([0, 0, 1])

    total_calc = needy_val + goodjob_val + military_val - wealth_val + regulation_val

    '''if total_calc < 0:
        happy_val = 0
    elif 0 <= total_calc < 5:
        happy_val = 1
    elif 5 <= total_calc < 10:
        happy_val = 2
    elif 10 <= total_calc < 16:
        happy_val = 3
    elif total_calc >= 16:
        happy_val = 4

    for i in range(5):
        if i != happy_val:
            current_row.append(0)
        elif i == happy_val:
            current_row.append(1)'''

    #total_calc = total_calc + happy_val

    if total_calc < min_calc:
        min_calc = total_calc

    if total_calc > max_calc:
        max_calc = total_calc

    current_row.append(total_calc)


    all_data.append(current_row)

    count = count + 1
        

range_calc = max_calc - min_calc

for row in all_data:
    rating = (row[len(row)-1] - min_calc) / range_calc
    row[len(row)-1] = round(rating * 10, 2)

            
with open("hackital_dataset.csv", "w") as f:
    f.write(csv_header + '\n')

    for row in all_data:
        row_string = ""
        for i, col in enumerate(row):
            if i == len(row)-1:
                row_string = row_string + str(col) + '\n'
            else:
                row_string = row_string + str(col) + ','

        f.write(row_string)
