-- function

doubleMe x = x + x
doubleUs x y = doubleMe x + doubleMe y

doubleSmallNumber x = if x > 100
						then x
						else x*2

doubleSmallNumber' x = (if x > 100 then x else x*2) + 1

-- list

list = [1, 2, 3, 4] ++ [5, 6]

helloWorld = "hello" ++ " " ++ "world"

list2 = [[1, 2, 3, 4], [5, 3, 3, 3], [1, 2, 2, 3, 4], [1, 2, 3]]

list2Head = head list2
list2Tail = tail list2
list2Last = last list2
list2Init = init list2

list3 = take 2 list2
