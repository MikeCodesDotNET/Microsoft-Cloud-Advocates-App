//
//  YNSearchTextField.swift
//  YNSearch
//
//  Created by YiSeungyoun on 2017. 4. 11..
//  Copyright © 2017년 SeungyounYi. All rights reserved.
//

import UIKit

open class YNSearchTextField: UITextField {
    override init(frame: CGRect) {
        super.init(frame: frame)
        
        self.initView()
    }
    
    required public init?(coder aDecoder: NSCoder) {
        super.init(coder: aDecoder)
    }
    
    open func initView() {
        self.leftViewMode = .always
        
        let searchImageViewWrapper = UIView(frame: CGRect(x: 0, y: 0, width: 20, height: 15))
        let searchImageView = UIImageView(frame: CGRect(x: 0, y: 0, width: 15, height: 15))
        let search = UIImage(named: "search", in: Bundle(for: YNSearch.self), compatibleWith: nil)
        searchImageView.image = search
        searchImageViewWrapper.addSubview(searchImageView)
        
        self.leftView = searchImageViewWrapper
        self.returnKeyType = .search
        self.placeholder = "Search"
        self.textColor = UIColor.black
        self.font = UIFont(name: "Avenir-Medium", size: 17)
        self.backgroundColor = UIColor(red:0.93, green:0.94, blue:0.95, alpha:1.0)
        self.layer.cornerRadius = self.frame.height / 2
        self.layer.masksToBounds = true
    }
    
}

open class YNSearchTextFieldView: UIView {
    open var ynSearchTextField: YNSearchTextField!
    open var cancelButton: UIButton!
    
    override init(frame: CGRect) {
        super.init(frame: frame)
        
        self.initView()
    }
    
    required public init?(coder aDecoder: NSCoder) {
        super.init(coder: aDecoder)
    }
    
    open func initView() {
        //Original size (allows for cancel)
        self.ynSearchTextField = YNSearchTextField(frame: CGRect(x: 0, y: 0, width: self.frame.width - 50, height: self.frame.height))
        
        self.addSubview(self.ynSearchTextField)
        
        self.cancelButton = UIButton(frame: CGRect(x: self.frame.width - 40, y: 0, width: 50, height: self.frame.height))
        self.cancelButton.titleLabel?.font = UIFont(name: "Avenir-Medium", size: 16)
        self.cancelButton.setTitleColor(AppColours().info, for: .normal)
        self.cancelButton.setTitleColor(UIColor.darkGray.withAlphaComponent(0.3), for: .highlighted)
        self.cancelButton.setTitle("Cancel", for: .normal)
        self.addSubview(self.cancelButton)
        
    }
    
    
    
}
