//
//  YNSearchView.swift
//  YNSearch
//
//  Created by YiSeungyoun on 2017. 4. 11..
//  Copyright © 2017년 SeungyounYi. All rights reserved.
//

import UIKit

class YNSearchView: UIView, SearchMainViewDelegate, SearchListViewDelegate {
    var delegate: SearchDelegate?
    
    var ynScrollView: UIScrollView!
    var searchMainView: YNSearchMainView!
    var suggestionsListView: SearchSuggestionsListView!
    
    override init(frame: CGRect) {
        super.init(frame: frame)
        
        self.ynScrollView = UIScrollView(frame: CGRect(x: 0, y: 0, width: self.frame.width, height: self.frame.height))
        
        self.searchMainView = YNSearchMainView(frame: CGRect(x: 0, y: 0, width: self.frame.width, height: self.frame.height))
        self.searchMainView.delegate = self
        self.ynScrollView.addSubview(self.searchMainView)
        
        self.suggestionsListView = SearchSuggestionsListView(frame: CGRect(x: 0, y: 0, width: self.frame.width, height: self.frame.height))
        self.suggestionsListView.searchListViewDelegate = self
        self.suggestionsListView.isHidden = true
        
        if let clearHistoryButton = self.searchMainView.clearHistoryButton {
        self.ynScrollView.contentSize = CGSize(width: self.frame.width, height: clearHistoryButton.frame.origin.y + clearHistoryButton.frame.height + 20)
        } else {
            self.ynScrollView.contentSize = CGSize(width: self.frame.width, height: self.frame.height)
        }
        self.ynScrollView.addSubview(self.suggestionsListView)
        
        self.addSubview(ynScrollView)
        
        
    }
    
    func ynSearchMainViewSearchHistoryChanged() {
        let size = CGSize(width: self.frame.width, height: self.searchMainView.clearHistoryButton.frame.origin.y + self.searchMainView.clearHistoryButton.frame.height + 20)
        self.ynScrollView.contentSize = size
        self.searchMainView.frame = CGRect(origin: CGPoint(x: 0, y: 0), size: size)
    }
    
    required public init?(coder aDecoder: NSCoder) {
        super.init(coder: aDecoder)
    }
    
    func scrollViewDidScroll() {
        self.delegate?.searchListViewDidScroll()
    }
    
    // MARK: - ynSearchMainView
    func ynCategoryButtonClicked(text: String) {
        self.delegate?.ynCategoryButtonClicked(text: text)
    }
    
    func ynSearchHistoryButtonClicked(text: String) {
        self.delegate?.ynSearchHistoryButtonClicked(text: text)
    }
    
    func searchListViewClicked(key: String) {
        self.delegate?.searchListViewClicked(key: key)
    }
    
    func searchListViewClicked(object: Any) {
        self.delegate?.searchListViewClicked(object: object)
    }
    
    func searchListView(_ ynSearchListView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        guard let cell = self.delegate?.searchListView(ynSearchListView, cellForRowAt: indexPath) else { return UITableViewCell() }
        return cell
    }

    func searchListView(_ ynSearchListView: UITableView, didSelectRowAt indexPath: IndexPath) {
        self.delegate?.searchListView(ynSearchListView, didSelectRowAt: indexPath)
    }
    
    func searchListViewDidScroll() {
        self.delegate?.searchListViewDidScroll()
    }
    




}
