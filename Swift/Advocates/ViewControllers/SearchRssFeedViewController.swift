//
//  SearchRssFeedViewController.swift
//  Advocates
//
//  Created by Michael James on 18/07/2019.
//  Copyright © 2019 Mike James. All rights reserved.
//

import Foundation
import UIKit

class SearchRssFeedViewController : SearchViewController, SearchDelegate {
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        let demoCategories = ["AI & ML", "Analytics", "Azure Stack", "Blockchain", "Compute", "Containers", "Databases", "Developer Tools", "DevOps", "Identity", "Integration", "IoT", "Media", "Migration", "Mixed Reality", "Mobile", "Networking", "Security", "Storage", "Web"]
        
        
        
        let demoSearchHistories = ["Menu", "Animation", "Transition", "TableView"]
        
        let ynSearch = Search()
        ynSearch.setCategories(value: demoCategories)
        searchService.setSearchHistories(value: demoSearchHistories)
        
        self.ynSearchinit()
        
        self.delegate = self
        self.navigationController?.setNavigationBarHidden(true, animated: false)
        
        
        let database1 = YNDropDownMenu(key: "DropDownMenu")
        let database2 = YNSearchData(key: "SearchData")
        let database3 = YNExpandableCell(key: "ExpandableCell")
        let demoDatabase = [database1, database2, database3]
        
        
        
        self.initData(database: demoDatabase)
        self.setYNCategoryButtonType(type: .colorful)
        
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    func searchListViewDidScroll() {
        self.searchTextfieldView.searchTextField.endEditing(true)
    }
    
    
    func ynSearchHistoryButtonClicked(text: String) {
        self.pushViewController(text: text)
        print(text)
    }
    
    func ynCategoryButtonClicked(text: String) {
        //self.pushViewController(text: text)
        
        
        
        print(text)
    }
    
    func searchListViewClicked(key: String) {
        self.pushViewController(text: key)
        print(key)
    }
    
    func searchListViewClicked(object: Any) {
        print(object)
    }
    
    func searchListView(_ ynSearchListView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = self.searchView.suggestionsListView.dequeueReusableCell(withIdentifier: SearchSuggestionListViewCell.ID) as! SearchSuggestionListViewCell
        if let searchResult = self.searchView.suggestionsListView.searchResultDatabase[indexPath.row] as? SearchResult {
            cell.searchResult = searchResult
            
            let cmp = searchResult.searchText
      
            if(cmp?.contains("[") == true) {
                cell.searchLabel.attributedText = hitHighlightText(searchText: searchResult.searchText!)
            } else {
                cell.searchLabel.text = searchResult.searchText
            }
        }
        
        return cell
    }
    
    func searchListView(_ ynSearchListView: UITableView, didSelectRowAt indexPath: IndexPath) {
        if let ynmodel = self.searchView.suggestionsListView.searchResultDatabase[indexPath.row] as? YNSearchModel, let key = ynmodel.key {
            self.searchView.suggestionsListView.searchListViewDelegate?.searchListViewClicked(key: key)
            self.searchView.suggestionsListView.searchListViewDelegate?.searchListViewClicked(object: self.searchView.suggestionsListView.database[indexPath.row])
            searchService.appendSearchHistories(value: key)
        }
    }
    
    
    func hitHighlightText(searchText: String) -> NSMutableAttributedString{
        
        var hitAttributes: [NSAttributedString.Key: Any] = [.font: UIFont(name: "Avenir-Black", size: 15)!]
        var defaultAttributes = [NSAttributedString.Key.foregroundColor: UIColor.darkGray]
        
        var attributedString: NSMutableAttributedString?
        
        if(searchText.hasPrefix("[")){
            //its first
            var spl = searchText.split(separator: "]")
            var hit = spl[0].dropFirst(1)
            var remaining = spl[1]
            
            
            let hitString = NSMutableAttributedString(string: String(hit), attributes: hitAttributes)
            let defaultString = NSAttributedString(string: String(remaining), attributes: defaultAttributes)
            
            hitString.append(defaultString)
            return hitString
            
        } else if(searchText.last == ("]")){
            //its first
            var frt = searchText.split(separator: "[")
            var remaining = frt[0]
            var hit = frt[1].dropLast()
            
            let defaultString = NSMutableAttributedString(string: String(remaining), attributes: defaultAttributes)
            
            let hitString = NSAttributedString(string: String(hit), attributes: hitAttributes)
            
            defaultString.append(hitString)
            return defaultString
        } else {
            
            var one = searchText.split(separator: "[")
            let preText = one[0]
            let hitPostText = one[1]
            
            
            let hitPostSplit = hitPostText.split(separator: "]")
            let hit = hitPostSplit[0]
            let postText = hitPostSplit[1]
            
            
            let preTextString = NSMutableAttributedString(string: String(preText), attributes: defaultAttributes)
            let hitString = NSAttributedString(string: String(hit), attributes: hitAttributes)
            let postTextString = NSAttributedString(string: String(postText), attributes: defaultAttributes)
            
            preTextString.append(hitString)
            preTextString.append(postTextString)
            return preTextString
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

