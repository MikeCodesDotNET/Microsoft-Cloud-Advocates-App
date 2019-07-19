//
//  YNSearchListViewCell.swift
//  YNSearch
//
//  Created by YiSeungyoun on 2017. 4. 16..
//  Copyright © 2017년 SeungyounYi. All rights reserved.
//

import UIKit

class SearchSuggestionListViewCell: UITableViewCell {
    public static let ID = "SearchListViewCell"
    
    var leftMargin = 15
    
    var searchResult: SearchResult?
    var searchImageView: UIImageView!
    var searchLabel: UILabel!
        
    override init(style: UITableViewCell.CellStyle, reuseIdentifier: String?) {
        super.init(style: style, reuseIdentifier: reuseIdentifier)
        
        self.initView()
    }
    
    required public init?(coder aDecoder: NSCoder) {
        super.init(coder: aDecoder)
    }
    
    func initView() {
        self.searchImageView = UIImageView(frame: CGRect(x: 20, y: (self.frame.height - 15)/2, width: 15, height: 15))
        let search = UIImage(named: "search", in: Bundle(for: Search.self), compatibleWith: nil)
        self.searchImageView.image = search
        self.addSubview(searchImageView)
        
        
        self.searchLabel = UILabel(frame: CGRect(x: 25, y: 0, width: self.frame.width - 20, height: self.frame.height))
        self.searchLabel.textColor = UIColor.darkGray
        self.searchLabel.font = UIFont(name: "Avenir-Medium", size: 14)
        self.addSubview(searchLabel)
    }
    
    
}
