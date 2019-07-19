//
//  SearchRssFeedViewController.swift
//  Advocates
//
//  Created by Michael James on 18/07/2019.
//  Copyright © 2019 Mike James. All rights reserved.
//

import Foundation
import UIKit

class SearchRssFeedViewController : SearchViewController, YNSearchDelegate {
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        let demoCategories = ["App Services", "AKS", "Cognitive Services", "AI", "Storage", "Analytics", "SQL", "Cosmos DB", "DevOps", "Networking", "Migration", "Data", "SDK", "VM"]
        let demoSearchHistories = ["Menu", "Animation", "Transition", "TableView"]
        
        let ynSearch = YNSearch()
        ynSearch.setCategories(value: demoCategories)
        ynSearch.setSearchHistories(value: demoSearchHistories)
        
        self.ynSearchinit()
        
        self.delegate = self
        self.navigationController?.setNavigationBarHidden(true, animated: false)
        
        let database1 = YNDropDownMenu(key: "YNDropDownMenu")
        let database2 = YNSearchData(key: "YNSearchData")
        let database3 = YNExpandableCell(key: "YNExpandableCell")
        let demoDatabase = [database1, database2, database3]
        
        self.initData(database: demoDatabase)
        self.setYNCategoryButtonType(type: .colorful)
        
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    func ynSearchListViewDidScroll() {
        self.SearchTextfieldView.ynSearchTextField.endEditing(true)
    }
    
    
    func ynSearchHistoryButtonClicked(text: String) {
        self.pushViewController(text: text)
        print(text)
    }
    
    func ynCategoryButtonClicked(text: String) {
        self.pushViewController(text: text)
        print(text)
    }
    
    func ynSearchListViewClicked(key: String) {
        self.pushViewController(text: key)
        print(key)
    }
    
    func ynSearchListViewClicked(object: Any) {
        print(object)
    }
    
    func ynSearchListView(_ ynSearchListView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = self.SearchView.ynSearchListView.dequeueReusableCell(withIdentifier: YNSearchListViewCell.ID) as! YNSearchListViewCell
        if let ynmodel = self.SearchView.ynSearchListView.searchResultDatabase[indexPath.row] as? YNSearchModel {
            cell.searchLabel.text = ynmodel.key
        }
        
        return cell
    }
    
    func ynSearchListView(_ ynSearchListView: UITableView, didSelectRowAt indexPath: IndexPath) {
        if let ynmodel = self.SearchView.ynSearchListView.searchResultDatabase[indexPath.row] as? YNSearchModel, let key = ynmodel.key {
            self.SearchView.ynSearchListView.ynSearchListViewDelegate?.ynSearchListViewClicked(key: key)
            self.SearchView.ynSearchListView.ynSearchListViewDelegate?.ynSearchListViewClicked(object: self.SearchView.ynSearchListView.database[indexPath.row])
            self.SearchView.ynSearchListView.ynSearch.appendSearchHistories(value: key)
        }
    }
    
    func pushViewController(text:String) {
        //let storyboard = UIStoryboard(name: "Main", bundle: nil)
       // let vc = storyboard.instantiateViewController(withIdentifier: "detail") as! DetailViewController
        //vc.clickedText = text
        
        //self.present(vc, animated: true, completion: nil)
    }
    
    
}

class YNDropDownMenu: YNSearchModel {
    var starCount = 512
    var description = "Awesome Dropdown menu for iOS with Swift 3"
    var version = "2.3.0"
    var url = "https://github.com/younatics/YNDropDownMenu"
}

class YNSearchData: YNSearchModel {
    var title = "YNSearch"
    var starCount = 271
    var description = "Awesome fully customize search view like Pinterest written in Swift 3"
    var version = "0.3.1"
    var url = "https://github.com/younatics/YNSearch"
}

class YNExpandableCell: YNSearchModel {
    var title = "YNExpandableCell"
    var starCount = 191
    var description = "Awesome expandable, collapsible tableview cell for iOS written in Swift 3"
    var version = "1.1.0"
    var url = "https://github.com/younatics/YNExpandableCell"
}

