//
//  BlogPost.swift
//  Advocates
//
//  Created by Michael James on 11/06/2019.
//  Copyright © 2019 Mike James. All rights reserved.
//

import Foundation

class BlogPost : NSObject {
    
    var Title : String?
    var Description : String?
    var IsFamilyFriendly : Bool?
    var PrimaryImage : String?
    var PublishedDate : NSDate?
    var Summary : String?
    var Url : String?
    var ClassType : String?
    var Identifier : String?
    
    required init(from dictionary: [AnyHashable : Any]) {
        //sdk turns dictionary to concrete Swift class
        self.Title = dictionary["title"] as? String
        self.Description = dictionary["description"] as? String
        self.IsFamilyFriendly = dictionary["isFamilyFriendly"] as? Bool
        self.PrimaryImage = dictionary["primaryImage"] as? String
        self.Summary = dictionary["summary"] as? String
        self.Url = dictionary["url"] as? String
        self.ClassType = dictionary["classType"] as? String
        self.Identifier = dictionary["identifier"] as? String

        

    }
    
   
}
