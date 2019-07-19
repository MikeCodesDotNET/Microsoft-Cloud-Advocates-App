//
//  YNCategoryButton.swift
//  YNSearch
//
//  Created by Seungyoun Yi on 2017. 4. 12..
//  Copyright © 2017년 SeungyounYi. All rights reserved.
//

import UIKit

public enum CategoryButtonType {
    case background
    case border
    case colorful
}

open class YNCategoryButton: UIButton {
    open var type: CategoryButtonType? {
        didSet {
            guard let _type = type else { return }
            self.setType(type: _type)
        }
    }
    override init(frame: CGRect) {
        super.init(frame: frame)
        
        self.initVIew()
    }
    
    required public init?(coder aDecoder: NSCoder) {
        super.init(coder: aDecoder)
    }
    
    override open var isHighlighted: Bool {
        didSet {
            if let type = self.type {
                switch type {
                case .border:
                    switch isHighlighted {
                    case true:
                        layer.borderColor = UIColor.lightGray.cgColor
                    case false:
                        layer.borderColor = UIColor.darkGray.cgColor
                    }

                case .colorful:
                    switch isHighlighted {
                    case true:
                        layer.borderColor = UIColor.lightGray.cgColor
                    case false:
                        layer.borderColor = UIColor.white.cgColor
                    }
                    
                case .background: break
                }
                
            } else {
                switch isHighlighted {
                case true:
                    layer.borderColor = UIColor.lightGray.cgColor
                case false:
                    layer.borderColor = UIColor.darkGray.cgColor
                }
            }
        }
    }
    open func initVIew() {
        self.layer.borderColor = UIColor.darkGray.cgColor
        self.layer.borderWidth = 1
        self.titleLabel?.font = UIFont(name: "Avenir-Medium", size: 14)
        self.setTitleColor(UIColor.darkGray, for: .normal)
        self.setTitleColor(UIColor.lightGray, for: .highlighted)
        self.layer.cornerRadius = self.frame.height * 0.1

    }
    
    open func setType(type: CategoryButtonType) {
        switch type {
        case .background:
            self.layer.borderColor = nil
            self.layer.borderWidth = 0
            self.backgroundColor = UIColor.init(red: 246.0/255.0, green: 246.0/255.0, blue: 246.0/255.0, alpha: 1)
            self.setTitleColor(UIColor.darkGray, for: .normal)
            self.setTitleColor(UIColor.darkGray.withAlphaComponent(0.3), for: .highlighted)
            
        case .border:
            self.layer.borderColor = UIColor.darkGray.cgColor
            self.layer.borderWidth = 1
            self.setTitleColor(UIColor.darkGray, for: .normal)
            self.setTitleColor(UIColor.darkGray.withAlphaComponent(0.3), for: .highlighted)
            
        case .colorful:

            self.backgroundColor = UIColor.white
            self.layer.borderWidth = 1
            self.layer.borderColor = UIColor(red:0.00, green:0.47, blue:0.83, alpha:1.0).cgColor
            self.setTitleColor(UIColor.darkGray, for: .normal)
            self.setTitleColor(UIColor.darkGray.withAlphaComponent(0.3), for: .highlighted)
        }
        
    }
    
    open func randomColor() -> UIColor {
        let colorArray = ["007AFF", "FDCA40", "D11149", "F17105", "541388", "93C0A4", "FF7733", "F85A3E", "FDB833", "FFD07B", "EE4266"]
        
        let randomNumber = arc4random_uniform(UInt32(colorArray.count))
        return UIColor(hexString: colorArray[Int(randomNumber)])
    }
    

}
