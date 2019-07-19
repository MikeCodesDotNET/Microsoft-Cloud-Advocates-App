//
//  YNSearch.swift
//  YNSearch
//
//  Created by YiSeungyoun on 2017. 4. 11..
//  Copyright © 2017년 SeungyounYi. All rights reserved.
//

import UIKit

class Search: NSObject {
    var pref: UserDefaults!
    
    public static let shared: Search = Search()

    public override init() {
        pref = UserDefaults.standard
    }
    
    func setCategories(value: [String]) {
        pref.set(value, forKey: "categories")
    }
    
    func getCategories() -> [String]? {
        guard let categories = pref.object(forKey: "categories") as? [String] else { return nil }
        return categories
    }

    
    

}
