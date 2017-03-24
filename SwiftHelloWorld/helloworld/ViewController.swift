//
//  ViewController.swift
//  helloworld
//
//  Created by KOBAYASHI Naoya on 2015/07/09.
//  Copyright (c) 2015年 KOBAYASHI Naoya. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
    }
    
    var countNum = 0
    var timerRunning = false
    var timer = NSTimer()
    
    func update() {
        countNum++
        timeFormat(countNum)
    }
    
    func timeFormat(countNum:Int) {
        let ms = countNum % 10
        let s = (countNum - ms) / 10
        var hello : String
        switch s {
        case 0..<10:
            hello = "Hell\(s)"
        case 10..<100:
            hello = "Hel\(s)"
        default:
            hello = "He\(s)"
        }
        helloWorld.text = "\(hello) W\(ms)rld"
    }
    
    @IBOutlet weak var helloWorld: UILabel!
    
    @IBAction func tapHandler(sender: AnyObject) {
        if timerRunning == false {
            timer = NSTimer.scheduledTimerWithTimeInterval(0.1, target: self, selector: Selector("update"), userInfo: nil, repeats: true)
            timerRunning = true
        }
    }
    
    @IBAction func resetHello(sender: AnyObject) {
        if timerRunning == true {
            timer.invalidate()
            timerRunning = false
        }
        
        countNum = 0
        helloWorld.text = "Hell0 W0rld"
    }
    
    // func
    func studyMain() {
        print("Hello World")
        
        // define
        let const = "hogefuga"
        var x : Int? = 1
        x = nil
        let arr : [Int] = [1, 1, 2, 3, 5]
        let dic : [String:String] = [
            "key1": "value1",
            "key2": "value2"
        ]
        var tpl = (1, true)
        let tpl2 = (hoge: "hoge", fuga: 123)
        
        // if
        if tpl2.fuga < 100 {
            // println("\(tpl2.fuga) < 100")
        } else if tpl2.fuga >= 100 && tpl2.fuga <= 200 {
            print("100 <= \(tpl2.fuga) <= 200")
        } else {
            print("200 < \(tpl2.fuga)")
        }
        
        // while
        var count = 0
        while count < 10 {
            print("\(count++) in while")
        }
        
        // for
        var sum = 0;
        for var i = 0; i < 10; i++ {
            sum += i
        }
        
        // for in
        // Range
        for i in 0...10 {
            sum += i
        }
        // with index
        for (index, element) in arr.enumerate() {
            sum += index * element
        }
        // dictionary
        for (key, value) in dic {
            print("\(key): \(value)")
        }
        
        // optional
        let y : Int? = 2;
        print("y + 1 = \(y! + 1)")
        
        if let n = y {
            print("y is not nil. y = \(n)")
        } else {
            print("y is nil")
        }
        
        let fruits = ["apple", "banana", "orange"]
        if let index = indexOf(fruits, value: "orange") {
            print("orange は \(index)番目")
        }
        
        // closure
        var hoge = { (x: Int) -> String in
            String(x)
        }
        hoge = { x in String(x) } // 型が明らかな場合は型宣言を省略可
        hoge = { String($0) } // クロージャの引数は$0〜
        var fuga = { (x: Int, y: Int -> String) -> String in
            let z = y(x)
            return z
        }
        
        myPrint(hoge(365))
    }
    
    
    func indexOf(arr: [String], value: String) -> Int? {
        for (i, x) in arr.enumerate() {
            if x == value {
                return i
            }
        }
        return nil
    }
    
    func myPrint(str: String) {
        print(str)
    }
    
    func double(value: Int) -> Int {
        return 2 * value
    }
    
    func toTuple(num: Int, str: String) -> (Int, String) {
        return (num, str)
    }
    
    func randomPrint(names: String...) {
        let length = names.count
        myPrint(names[Int(arc4random()) % length])
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }


}

