//: Playground - noun: a place where people can play

import UIKit

var str = "Hello, playground"
print(str)

let value1 :Int = 3
let value2 = 0.14159
let pi = Double(value1) + value2

var array1 :[String] = [String]()
array1.append("テスト1")
array1.append("テスト2")

let array2 = ["あ", "い", "う"]

var dic1 = ["1+1": 2, "1+3": 4]

var text :String?
text = "hello"
print(text)
if let text = text {
    print(text)
}
print(text)

func twiceAndAddOne(number value :Int) -> Int {
    let value2 :Int = 1
    return value * 2 + value2
}

twiceAndAddOne(number: 3)

enum MyError :ErrorType {
    case InvalidValue
    case Unexpected
    case NotImplemented
}

func doubleUp(value :Int) throws -> Int {
    if value < 0 {
        throw MyError.InvalidValue
    }
    return value * 2
}

do {
    defer {
        print("処理終了時間: \(NSDate())")
    }
    print("処理開始時間: \(NSDate())")
    var doubleResult = try doubleUp(-10)
} catch {
    print("NG")
}

typealias ColorCode = UInt8
let r = ColorCode.max
let g = ColorCode.min
let b = ColorCode.max
print("\(r), \(g), \(b)")


var radio = UISwitch()
radio.on = true
radio.setOn(false, animated: true)


class Dog {
    var name = ""
    var age = 1
    
    func bark() {
    }
}

var dog = Dog()
dog.name = "キング"
dog.bark()

func requestMinAndMax() -> (min :Int, max :Int) {
    return (0, 100)
}

let minAndMax = requestMinAndMax()
print("\(minAndMax.min)〜\(minAndMax.max)")

for num in 0 ..< 10 {
    print("num: \(num)")
}

let error = MyError.Unexpected
switch error {
case .Unexpected:
    print(1)
    fallthrough
case .NotImplemented:
    print(2)
case .InvalidValue:
    print(3)
}
