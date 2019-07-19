//
//  YNSearchViewController.swift
//  YNSearch
//
//  Created by YiSeungyoun on 2017. 4. 11..
//  Copyright © 2017년 SeungyounYi. All rights reserved.
//

import UIKit

class SearchViewController: UIViewController, UITextFieldDelegate {
    var delegate: SearchDelegate? {
        didSet {
            self.searchView.delegate = delegate
        }
    }
    
    let width = UIScreen.main.bounds.width
    let height = UIScreen.main.bounds.height
    
    var searchTextfieldView: YNSearchTextFieldView!
    var searchView: YNSearchView!
    var titleLabel: UILabel!
    
    var ynSerach = Search()

    override func viewDidLoad() {
        super.viewDidLoad()
        
    }
    
    override func viewDidLayoutSubviews() {
        super.viewDidLayoutSubviews()
        
        var safeAreaTopInset: CGFloat = 0
        if #available(iOS 11, *) {
            safeAreaTopInset = view.safeAreaInsets.top
        }
        
        //Called second
        self.searchTextfieldView.frame = CGRect(x: 20, y: safeAreaTopInset + 110, width: width - 20, height: 35)
        self.searchView.frame = CGRect(x: 0, y: 160 + safeAreaTopInset, width: width, height: height - 70 - safeAreaTopInset)
    }

    func ynSearchinit() {
        
        //Called first
        self.searchTextfieldView = YNSearchTextFieldView(frame: CGRect(x: 20, y: 110, width: width - 40, height: 35))
        self.searchTextfieldView.searchTextField.delegate = self
        self.searchTextfieldView.searchTextField.addTarget(self, action: #selector(ynSearchTextfieldTextChanged(_:)), for: .editingChanged)
        
        self.searchTextfieldView.cancelButton.addTarget(self, action: #selector(ynSearchTextfieldcancelButtonClicked), for: .touchUpInside)
        
        self.searchTextfieldView.searchTextField.clearButtonMode = UITextField.ViewMode.whileEditing
        
        self.view.addSubview(self.searchTextfieldView)
        
        self.searchView = YNSearchView(frame: CGRect(x: 0, y: 70, width: width, height: height - 70))
        self.view.addSubview(self.searchView)
        
        self.titleLabel = UILabel.init(frame: CGRect(x: 20, y: 50, width: width - 40, height: 35))
        self.titleLabel.font = UIFont(name: "Avenir-Heavy", size: 32)
        self.titleLabel.text = "Search"
        self.view.addSubview(self.titleLabel)
        
        
    }
    
    func setYNCategoryButtonType(type: CategoryButtonType) {
        self.searchView.searchMainView.setYNCategoryButtonType(type: .colorful)
    }
    
    func initData(database: [Any]) {
        self.searchView.suggestionsListView.initData(database: database)
    }

    
    // MARK: - YNSearchTextfield
    @objc func ynSearchTextfieldcancelButtonClicked() {
        self.searchTextfieldView.searchTextField.text = ""
        self.searchTextfieldView.searchTextField.endEditing(true)
        self.searchView.searchMainView.redrawSearchHistoryButtons()
        
        
        
        UIView.animate(withDuration: 0.3, animations: {
            self.searchView.searchMainView.alpha = 1
            self.searchTextfieldView.cancelButton.alpha = 0
            self.searchView.suggestionsListView.alpha = 0
        }) { (true) in
            self.searchView.searchMainView.isHidden = false
            self.searchView.suggestionsListView.isHidden = true
        }
        
        self.dismiss(animated: true, completion: {})
        
    }
    @objc open func ynSearchTextfieldTextChanged(_ textField: UITextField) {
        
        let text = textField.text
        
        self.searchView.suggestionsListView.searchTextFieldText = text
        
        if text == "" {
            DispatchQueue.main.async {
                self.searchView.suggestionsListView.searchResultDatabase = [SearchResult]()
                self.searchView.suggestionsListView.reloadData()
            }
            return
        }
        
         self.searchService.suggest(query: text!, completion: { results in
            DispatchQueue.main.async {
                self.searchView.suggestionsListView.database = results
                self.searchView.suggestionsListView.searchResultDatabase = results
                self.searchView.suggestionsListView.reloadData()
            }
            
         })
    }
    
    // MARK: - UITextFieldDelegate
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        guard let text = textField.text else { return true }
        if !text.isEmpty {
            self.searchService.appendSearchHistories(value: text)
            self.searchView.searchMainView.redrawSearchHistoryButtons()
            
            
            self.searchService.search(query: text, completion: { results in
                DispatchQueue.main.async {
                    self.searchView.suggestionsListView.database = results
                    self.searchView.suggestionsListView.searchResultDatabase = results
                    self.searchView.suggestionsListView.reloadData()
                }
            })
            
        }
        self.searchTextfieldView.searchTextField.endEditing(true)
        return true
    }
    
    
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        UIView.animate(withDuration: 0.3, animations: {
            self.searchView.searchMainView.alpha = 0
            self.searchTextfieldView.cancelButton.alpha = 1
            self.searchView.suggestionsListView.alpha = 1
            
        }) { (true) in
            self.searchView.searchMainView.isHidden = true
            self.searchView.suggestionsListView.isHidden = false
        }
        
        
        
    }
    
    let searchService = SearchService.init(indexName: "blog-posts")
}
