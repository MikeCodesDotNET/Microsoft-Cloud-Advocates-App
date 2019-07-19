//
//  YNSearchViewController.swift
//  YNSearch
//
//  Created by YiSeungyoun on 2017. 4. 11..
//  Copyright © 2017년 SeungyounYi. All rights reserved.
//

import UIKit

open class SearchViewController: UIViewController, UITextFieldDelegate {
    open var delegate: YNSearchDelegate? {
        didSet {
            self.SearchView.delegate = delegate
        }
    }
    
    let width = UIScreen.main.bounds.width
    let height = UIScreen.main.bounds.height
    
    open var SearchTextfieldView: YNSearchTextFieldView!
    open var SearchView: YNSearchView!
    
    open var ynSerach = YNSearch()

    override open func viewDidLoad() {
        super.viewDidLoad()
        
    }
    
    override open func viewDidLayoutSubviews() {
        super.viewDidLayoutSubviews()
        
        var safeAreaTopInset: CGFloat = 0
        if #available(iOS 11, *) {
            safeAreaTopInset = view.safeAreaInsets.top
        }
        
        //Called second
        self.SearchTextfieldView.frame = CGRect(x: 20, y: safeAreaTopInset + 20, width: width - 20, height: 35)
        self.SearchView.frame = CGRect(x: 0, y: 70 + safeAreaTopInset, width: width, height: height - 70 - safeAreaTopInset)
    }

    open func ynSearchinit() {
        
        //Called first
        self.SearchTextfieldView = YNSearchTextFieldView(frame: CGRect(x: 20, y: 20, width: width - 40, height: 35))
        self.SearchTextfieldView.ynSearchTextField.delegate = self
        self.SearchTextfieldView.ynSearchTextField.addTarget(self, action: #selector(ynSearchTextfieldTextChanged(_:)), for: .editingChanged)
        self.SearchTextfieldView.cancelButton.addTarget(self, action: #selector(ynSearchTextfieldcancelButtonClicked), for: .touchUpInside)
        
        self.SearchTextfieldView.ynSearchTextField.clearButtonMode = UITextField.ViewMode.whileEditing
        
        self.view.addSubview(self.SearchTextfieldView)
        
        self.SearchView = YNSearchView(frame: CGRect(x: 0, y: 70, width: width, height: height - 70))
        self.view.addSubview(self.SearchView)
        
        //TODO remove this as its jsut for testing
        let searchService = SearchService.init(indexName: "blog-posts")
        let results = searchService.search(query: "HDInsight", completion: { documents in
            for searchResult in documents {
                print(searchResult.title)
            }
        })
        
        
        //TODO remove the testing of suggeastions
        
        let suggestions = searchService.suggest(query: "HDInsigh", completion: { documents in
            for searchResult in documents {
                print(searchResult.title)
            }
        })
        
        
    }
    
    open func setYNCategoryButtonType(type: CategoryButtonType) {
        self.SearchView.ynSearchMainView.setYNCategoryButtonType(type: .colorful)
    }
    
    open func initData(database: [Any]) {
        self.SearchView.ynSearchListView.initData(database: database)
    }

    
    // MARK: - YNSearchTextfield
    @objc open func ynSearchTextfieldcancelButtonClicked() {
        self.SearchTextfieldView.ynSearchTextField.text = ""
        self.SearchTextfieldView.ynSearchTextField.endEditing(true)
        self.SearchView.ynSearchMainView.redrawSearchHistoryButtons()
        
        
        
        UIView.animate(withDuration: 0.3, animations: {
            self.SearchView.ynSearchMainView.alpha = 1
            self.SearchTextfieldView.cancelButton.alpha = 0
            self.SearchView.ynSearchListView.alpha = 0
        }) { (true) in
            self.SearchView.ynSearchMainView.isHidden = false
            self.SearchView.ynSearchListView.isHidden = true
        }
        
        self.dismiss(animated: true, completion: {})
        
    }
    @objc open func ynSearchTextfieldTextChanged(_ textField: UITextField) {
        self.SearchView.ynSearchListView.ynSearchTextFieldText = textField.text
    }
    
    // MARK: - UITextFieldDelegate
    open func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        guard let text = textField.text else { return true }
        if !text.isEmpty {
            self.ynSerach.appendSearchHistories(value: text)
            self.SearchView.ynSearchMainView.redrawSearchHistoryButtons()
        }
        self.SearchTextfieldView.ynSearchTextField.endEditing(true)
        return true
    }
    
    open func textFieldDidBeginEditing(_ textField: UITextField) {
        UIView.animate(withDuration: 0.3, animations: {
            self.SearchView.ynSearchMainView.alpha = 0
            self.SearchTextfieldView.cancelButton.alpha = 1
            self.SearchView.ynSearchListView.alpha = 1
            
        }) { (true) in
            self.SearchView.ynSearchMainView.isHidden = true
            self.SearchView.ynSearchListView.isHidden = false
        }
        
        
        
    }
}
