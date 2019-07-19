//
//  YNSearchDelegate.swift
//  YNSearch
//
//  Created by YiSeungyoun on 2017. 4. 16..
//  Copyright © 2017년 SeungyounYi. All rights reserved.
//

import UIKit

public protocol SearchDelegate: SearchMainViewDelegate, SearchListViewDelegate { }

public protocol SearchMainViewDelegate {
    func ynCategoryButtonClicked(text: String)
    
    func ynSearchHistoryButtonClicked(text: String)

    func ynSearchMainViewSearchHistoryChanged()

}

public protocol SearchListViewDelegate {
    
    func searchListViewClicked(key: String)
    
    func searchListViewClicked(object: Any)
    
    func searchListView(_ ynSearchListView: UITableView, didSelectRowAt indexPath: IndexPath)
    
    func searchListView(_ ynSearchListView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell
    
    func searchListViewDidScroll()

    
}

public extension SearchMainViewDelegate {
    func ynSearchMainViewSearchHistoryChanged() { }
}

public extension SearchListViewDelegate {
    func searchListViewClicked(object: Any) { }
    
    func searchListViewDidScroll() { }

}


