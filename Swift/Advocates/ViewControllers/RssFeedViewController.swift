//
//  RssFeedViewController.swift
//  Advocates
//
//  Created by Michael James on 17/07/2019.
//  Copyright © 2019 Mike James. All rights reserved.
//

import Foundation
import SafariServices
import UIKit
import SPStorkController
import Kingfisher

class RssFeedViewController : UITableViewController, UIViewControllerPreviewingDelegate {
    
    
    func previewingContext(_ previewingContext: UIViewControllerPreviewing, viewControllerForLocation location: CGPoint) -> UIViewController? {
        
        if let indexPath = tableView.indexPathForRow(at: location) {
            previewingContext.sourceRect = tableView.rectForRow(at: indexPath)
      
            //Get the blog post item
            let index = tableView.indexPathForRow(at: location)
            let blogPost = viewModel.blogPosts[(index?.row)!]
            
            let url = URL.init(string: blogPost.url)!
            
            let config = SFSafariViewController.Configuration()
            config.entersReaderIfAvailable = true
            config.barCollapsingEnabled = true
            
             let vc = SFSafariViewController(url: url, configuration: config )
            
            return vc
        }
        
        return nil
    }
    
    func previewingContext(_ previewingContext: UIViewControllerPreviewing, commit viewControllerToCommit: UIViewController) {
        
        present(viewControllerToCommit, animated: true)

    }
    
    
    // MARK: - UIViewControllerPreviewingDelegate
    
    
    // MARK: - UIViewController Overrides
    override func viewDidLoad() {
        
        registerForPreviewing(with: self, sourceView: tableView)

        viewModel.refresh(completionHandler: {
            DispatchQueue.main.async {
                self.tableView.reloadData()
            }
        })
        
        
    }
    
    @IBAction func searchButtonTapped(_ sender: Any) {
        let vc = storyboard?.instantiateViewController(withIdentifier: "SearchRssFeed")
        
        presentAsStork(vc!)
    }
    
    // MARK: - UITableViewController Overrides
    override func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }
    
    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return viewModel.blogPosts.count
    }
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell = tableView.dequeueReusableCell(withIdentifier: "postCell", for: indexPath) as! BlogPostTableViewCell
        
        let blogPost = viewModel.blogPosts[indexPath.row]
        
        let url = URL(string: blogPost.primaryImage.contentUrl)
        cell.primaryImageView.kf.setImage(with: url, options: [.transition(.fade(0.5))])
        cell.primaryImageView.layer.cornerRadius = 8
        cell.primaryImageView.layer.borderWidth = 1
        cell.primaryImageView.layer.borderColor = UIColor.clouds.cgColor
        
        cell.primaryImageView.layer.shadowColor = UIColor.black.cgColor
        cell.primaryImageView.layer.shadowOpacity = 1
        cell.primaryImageView.layer.shadowOffset = .zero
        cell.primaryImageView.layer.shadowRadius = 10
        
        cell.titleLabel.text = blogPost.title
        cell.sourceLabel.text = blogPost.source
        return cell
    }
    
   
    override func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        let blogPost = viewModel.blogPosts[indexPath.row]
        if let url = URL(string: blogPost.url) {
            let config = SFSafariViewController.Configuration()
            config.entersReaderIfAvailable = true
            config.barCollapsingEnabled = true
            
            let vc = SFSafariViewController(url: url, configuration: config)
            vc.preferredBarTintColor = UIColor.black
            vc.preferredControlTintColor = UIColor.white
            present(vc, animated: true)
            
        }
    }
    
    // MARK: - Fields
    let viewModel = RssFeedViewModel()
    
}
