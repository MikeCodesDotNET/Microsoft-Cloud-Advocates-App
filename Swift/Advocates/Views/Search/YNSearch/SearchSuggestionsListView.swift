//
//  YNSearchListView.swift
//  YNSearch
//
//  Created by YiSeungyoun on 2017. 4. 16..
//  Copyright © 2017년 SeungyounYi. All rights reserved.
//

import UIKit
import SafariServices

class SearchSuggestionsListView: UITableView, UITableViewDelegate, UITableViewDataSource, UIScrollViewDelegate {
    
    var database = [Any]()
    var searchResultDatabase = [Any]()
    
    var searchListViewDelegate: SearchListViewDelegate?
    var search = Search()
    var searchTextFieldText: String? {
        didSet {
            guard let text = searchTextFieldText else { return }
            
            let objectification = Objectification(objects: database, type: .all)
            let result = objectification.objects(contain: text)

            self.searchResultDatabase = result
            if text.isEmpty {
                self.initData(database: database)
            }
            self.reloadData()
        }
    }

    public override init(frame: CGRect, style: UITableView.Style) {
        super.init(frame: frame, style: style)
        self.initView()
    }
    
    func initData(database: [Any]) {
        self.database = database
        self.searchResultDatabase = database
        self.reloadData()

    }
    
    required public init?(coder aDecoder: NSCoder) {
        super.init(coder: aDecoder)
    }
    
    // MARK: - UITableViewDelegate, UITableViewDataSource
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        guard let cell = self.searchListViewDelegate?.searchListView(tableView, cellForRowAt: indexPath) else { return UITableViewCell() }
        return cell
    }
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return searchResultDatabase.count
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        tableView.deselectRow(at: indexPath, animated: true)
        
        var result = searchResultDatabase[indexPath.row] as! SearchResult
        
        if let url = URL(string: result.url) {
            let config = SFSafariViewController.Configuration()
            config.entersReaderIfAvailable = true
            config.barCollapsingEnabled = true
            
            let vc = SFSafariViewController(url: url, configuration: config)
            vc.preferredBarTintColor = UIColor.black
            vc.preferredControlTintColor = UIColor.white
            DispatchQueue.main.async {
                
                if let topVC = UIApplication.getTopViewController() {
                    topVC.present(vc, animated: true)
                }
            }
            
        }
        
    }
    
    func scrollViewDidScroll(_ scrollView: UIScrollView) {
        self.searchListViewDelegate?.searchListViewDidScroll()
    }
    
    
    func initView() {
        self.delegate = self
        self.dataSource = self
        self.register(SearchSuggestionListViewCell.self, forCellReuseIdentifier: SearchSuggestionListViewCell.ID)
    }
    
    let searchService = SearchService.init(indexName: "blog-posts")
}
