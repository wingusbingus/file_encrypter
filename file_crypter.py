import base64
import os


def encrypt():
    image = input("File name: ")
    key = input("Key: ")

    if os.path.isdir(image):
        exit("[ERROR]: Cannot encrypt folders")

    with open(image, 'rb') as image2string:
        string = base64.b64encode(image2string.read())

    print("Encrypting . . .")

        #   ENC    #
    keyValue = ['none'] * len(key)
    encodedString = ['none'] * len(string)

    c = 0
    for char in key:
        keyValue[c] = ord(char)
        c += 1

    c = 0
    for char in string:
        encodedString[c] = ord(chr(char)) - 32
        encodedString[c] += keyValue[c % len(key)]
        encodedString[c] %= 94
        encodedString[c] += 32
        encodedString[c] = chr(int(encodedString[c]))
        c += 1
        #   ENC END    #

    string = ''.join(encodedString)

    with open(f'{image}', 'w') as file:
        file.write(string)

    input(f'File encrypted with key "{key}".')


def decrypt():
    image = input("File name: ")
    key = input("Key: ")

    file = open(f'{image}', 'r')
    string = file.read()
    file.close()

    print("Decrypting . . .")

            #   ENC    #
    keyValue = ['none'] * len(key)
    decodedString = ['none'] * len(string)

    c = 0
    for char in key:
        keyValue[c] = ord(char)
        c += 1

    c = 0
    for char in string:
        decodedString[c] = ord(char) - 32
        decodedString[c] -= keyValue[c % len(key)]
        while decodedString[c] < 0:
            decodedString[c] += 94
        decodedString[c] += 32
        decodedString[c] = chr(int(decodedString[c]))
        c += 1
        #   ENC END    #
    string = ''.join(decodedString)

    decoded = open(f"DECRYPTED_{image}", 'wb')
    decoded.write(base64.b64decode(string))
    decoded.close()

    input(f'File decrypted with key "{key}".')


try:
    while True:
        intention = input('Enter 1 to encrypt\nEnter 2 to decrypt\nInput: ')
        if intention == "1":
            print()
            encrypt()
            break
        elif intention == "2":
            print()
            decrypt()
            break
        else:
            print('[ERROR]: Failed to recognize input\n')
except Exception as e:
    input(e)
