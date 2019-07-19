//
//  RssFeedViewModel.swift
//  Advocates
//
//  Created by Michael James on 17/07/2019.
//  Copyright © 2019 Mike James. All rights reserved.
//

import Foundation
import AppCenterData


class RssFeedViewModel {
    
    var blogPosts: [BlogPost] = []
    
    func refresh(completionHandler: @escaping () -> ()){
        self.blogPosts.removeAll()
        
        MSData.listDocuments(withType: BlogPost.self, partition: kMSDataAppDocumentsPartition, completionHandler: { documents in
            for document in documents.currentPage().items {
                var fetchedDocument: BlogPost
                fetchedDocument = document.deserializedValue as! BlogPost
                self.blogPosts.append(fetchedDocument)
            }
            self.blogPosts = self.blogPosts.reversed()
            completionHandler()
        })
    }
    
}
