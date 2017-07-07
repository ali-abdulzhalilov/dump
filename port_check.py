import socket as sk
for port in range(1, 1024):
    s = sk.socket(sk.AF_INET, sk.SOCK_STREAM)
    s.settimeout(10)
    try:
        s.connect(('127.0.0.1',port))
        print(str(port)+":OPEN")
    except:
        continue
    s.close
