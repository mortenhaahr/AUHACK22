with open("SizeWeight_old.csv", "rb") as f:
    k = bytes(
        [x for x in f.read() if x < 58 or x in range(65, 91) or x in range(97, 123)]
    )
    with open("SizeWeight.csv", "wb") as wf:
        wf.write(k)
